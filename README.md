# clerk-data

## Purpose
This repository contains a service which processes a MemberData XML file by inputting the file's url, a script to create a PostgreSQL database to store the data in the file, and a simple UI to upload a file and view the data.

## Design choices and issues
Data Model:
- After looking at the XML file provided, I decided to design data models for MemberData (the root element), Member (the elements inside the member-info element), Committee (the elements inside the committees element), and SubCommittee. By capturing these elements and the relationships between them. I was confident that I could fully represent everything that was in the XML file.
- The main issue with creating these data models was that it consumed a large amount of time since there were a lot of data to capture. For example, the Member element contained a nested object that had more than 20 fields that I had to type out when creating the model.  

Service:
- For the service I assumed I needed to implement a feature complete API that provided CRUD functionalities for each data model, so I created a RESTful API service with .NET Core 3.1. However, after reading the instructions and the XML file I did not think I could implement a feature complete service over one weekend, so I had to determine and focus on the core goal of the exercise, and limited the scope of the service to 1) uploading file content to the database and 2) retrieving the MemberData object.
- I felt like I could have optimized the memberdata GET method if I had more time. I thought I had a little too many foreach loops calling the database. I could alternatively front-loaded some of the data and query the information I needed with LINQ. 
- One particular design choice I regretted was making subCommittee a subclass of committee under the committee-assignments element. I initially made that choice so I could fit both elements in one list when parsing the elements from XML. However, they all showed up as committee objects when I called the retrieve method from the swagger page.

Database:
- I picked a relational database for this exercise because there were a lot of relationships between the data models. Member <-> committee, committee <-> subcommittee, etc, etc. I chose PostgreSql specifically for its ease of use and docker support.
- I simplified the database structure by flattening some of the data models when I was creating the table for them. For example, I put all the fields of MemberData's title-info element in MemberData table because I felt making a table for every element and sub-element would made the database structure complicated and lead to confusion.

Other Issues:
- I had some issues making connections between the service and database after I started the docker containers. I got an exception saying the service "cannot assign requested address". I was able to solve it based on a [stack overflow article](https://stackoverflow.com/questions/59224272/connect-cannot-assign-requested-address). The basic explanation was that I could not use `localhost` in my config files. Instead, I needed to replace it with the name I assigned a infrastructure in the docker-compose.yml file. 

## Getting Started

### Prerequisites

- dotnet
- docker

### Setting Up

1. Clone the repository
2. Go to the folder
3. Spin up the containers
```
git clone https://github.com/ricecrispy/clerk-data.git
cd clerk-data
docker compose up
```

### Basic Usage
After you get the containers running, open a web browser and navigate to http://localhost:5555/ to access the web UI. You can upload a XML file by inputting its url, and view any memberdata that is already in the database.

### Infrastructure

Service:

The service can be accessed by going to http://localhost:5000/ after you spin up the containers (see Setting up for more details).

There are two methods - POST /memberdata and GET /memberdata.

`Post`:
- The Post method can be accessed by making a POST HTTP request to http://localhost:5000/memberdata?xmlUrl={some-url-to-a-xml-file}. This request uploads the content of the input XML file to the database.
- `xmlUrl` is a query parameter for a url link to a XML file.
- The method returns 204 if it completes successfully.

`Get`:
- The Get method can be accessed by making a GET HTTP request to http://localhost:5000/memberdata. This request retrieves all MemberData objects and returns it.
- The method returns 200 and the MemberData objects if it completes successfully.

There is also a swagger doc available at http://localhost:5000/api.

Database:

The database can be accessed by making a connection to 127.0.0.1:5432 after you spin up the containers.

The database is a PostgreSql database with two schemas - info and data. The info schema contains non-relational data, and the data schema contains relational data.

`info` schema:
- committee table: this table represents the committee object
- subcommittee table: this table represents the subommittee object
- memberdata table: this table represents the memberdata object (the root object of the XML file)
- member table: this table represents the member object
- nationstate table: this table contains information all the states in the U.S.

`data` schema:
- committeesubcommitteeassociation table: this table represents the one-to-many relationship between committee and subcommittee
- membercommitteeassociation table: this table represents the one-to-many relationship between mamber and committee
- memberdatacommitteeassociation table: this table represents the one-to-many relationship between memberdata and committee
- memberdatamemberassociation table: this table represents the one-to-many relationship between memberdata and member

Web UI:

The web UI can be accessed by going to http://localhost:5555/ after you spin up the containers. This UI is a simple tool for you to upload XML files and viewing memberdata.

When you access the web UI, there is a text field labeled "URL" where you can enter a URL of a XML file. When you enter the url in the text field and press the "Submit" button next to it, the UI makes a HTTP POST request to the service to upload the file content. Then the UI refreshes and the memberdata would be available to view under the "MemberData:" section.