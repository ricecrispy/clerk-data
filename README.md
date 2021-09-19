# clerk-data

## Purpose
This repository contains a service which processes a MemberData XML file by inputting the file's url, a script to create a PostgreSQL database to store the data in the file, and a simple UI to upload a file and view the data.

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