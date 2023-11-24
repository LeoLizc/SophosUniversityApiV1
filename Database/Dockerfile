# Use the official SQL Server 2019 image from Microsoft
FROM mcr.microsoft.com/mssql/server:2022-latest

# Set environment variables
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=00PrimeraPruebaPass1197

# Create a directory for SQL Server scripts
WORKDIR /usr/src/app

# Copy SQL Server scripts into the container
COPY database_creation_script.sql .
COPY database_population.sql .
COPY database_autorization.sql .

# Grant permissions to the scripts
USER root
RUN chmod +r database_creation_script.sql database_population.sql database_autorization.sql
USER mssql

# Start SQL Server and execute the scripts on container startup
RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -d master -i database_creation_script.sql -i database_population.sql -i database_autorization.sql
