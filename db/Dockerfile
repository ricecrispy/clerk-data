FROM postgres
ENV POSTGRES_PASSWORD Docker
ENV POSTGRES_DB clerkdata
COPY clerkdata.sql /docker-entrypoint-initdb.d/

# docker build -t clerk-data-db .
# docker run -dp 5432:5432 clerk-data-db