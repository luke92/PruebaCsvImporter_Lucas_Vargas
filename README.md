# PruebaCsvImporter_Lucas_Vargas
NET Core console program in C#, which reads a .csv file stored in an Azure storage account and inserts its content in a local SQL Server database.

# Data Source
- The .csv file is available at https://storage10082020.blob.core.windows.net/y9ne9ilzmfld/Stock.CSV
- It is separated by `;`
- The file has a size of 637 MB
- Approximate number of records: 1048576

# Performance and statistics
- Approximate time to import the complete file: 72hs
- CPU Usage: 33%
- Memory Usage: 40MB
- Maximum number of records inserted per sencond: 252
- Minimum number of records inserted per second: 1
- Average number of records inserted per second: 6
- During the first 5 minutes the number of records that are inserted per second are between 252 and 20
- At minute 10 the amount drops between 10 and 20
- Then it starts to go down until it reaches 4 records per second

# Stock Entity
| PointOfSale | Product | Date | Stock |
|--|--|--|--|
| Integer | String | DateTime | Integer

# Requirements
- Net Core 3.1 : https://dotnet.microsoft.com/download/dotnet/3.1
- SQL SERVER Local DB : https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15

# How to Run
- Use Package Manager Console Located in **CsvImporter.Core** and Run `Update-Database`
- Configure your **appsettings.json** located in **CsvImporter.ConsoleApp**
	- **connectionString** : Connection String of your database
	- **dataSource** : Path of **Stock.CSV**
- Run **CsvImporter.ConsoleApp**
