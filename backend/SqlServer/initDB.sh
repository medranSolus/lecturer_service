#!/bin/bash

# Wait to be sure that SQL Server came up
sleep 90s

# Run the setup script to create the DB and the schema in the DB
echo Creating Database...
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P OhYouTuchMaTralala12 -d master -i creation_script.sql
# Fill it with example data
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P OhYouTuchMaTralala12 -d master -i fill_db.sql