# run the following commands in a terminal to start the db container:
# docker build -t clerk-data-db .
# docker run -dp 5432:5432 clerk-data-db

FROM postgres
ENV POSTGRES_PASSWORD docker
ENV POSTGRES_DB clerkdata
COPY clerkdata.sql /docker-entrypoint-initdb.d/