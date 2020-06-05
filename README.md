# Time Logger
Ability to log in and out times in to a database for timesheets.

## Prerequisites
Docker for [Windows or Linux](https://docs.docker.com/get-docker/)

## Running the application

### Steps
Execute `docker-compose up -d` to run the application and postgres database as containers. A swagger page can be reached from `http://localhost/swagger/`

## Environment settings
The `docker-compose.yml` file includes a number of environment variables to configure postgres and the application.

### Postgres environment
1. `POSTGRES_USER` - username to access the postgres server
2. `POSTGRES_PASSWORD` - password for the user
3. `POSTGRES_DB` - the initial name of the database. Please leave this as `TimeLogger`

### Postgres volumes
The volume `pgdata` is mapped to the postgres container `/var/lib/postgresql/data` folder. The `pgdata` is a local volume. To inspect the volumes use command `sudo docker volume ls` and `sudo docker volume inspect <volume name>`. You can use the mountpoint location to make back ups of the data.

### Dotnet environment
1. `TZ` - local timezone
2. `TimeLogger_ConnectionString__PostgresServer` - postgres server name
3. `TimeLogger_ConnectionString__PostgresUser` - username to access the postgres server
4. `TimeLogger_ConnectionString__PostgresPassword` - password for the user
5. `TimeLogger_PublicHolidaySettings__ApiUrl` - url to aus public holiday [api](https://data.gov.au/dataset/ds-dga-b1bc6077-dadd-4f61-9f8c-002ab2cdff10/details)
6. `TimeLogger_PublicHolidaySettings__ResourceId` - year id of public holiday
7. `TimeLogger_PublicHolidaySettings__State` - australian state filter

## Todo
1. Implement Polly retry policies for added resiliency
2. Include an ELK stack in docker-compose to monitor the application
3. Nice to have: An added jenkins instance for CI/CD