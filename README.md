# PruebaCsvImporter_Lucas_Vargas
NET Core console program in C#, which reads a .csv file stored in an Azure storage account and inserts its content in a local SQL Server database.

# Data Source
- The .csv file is available at https://storage10082020.blob.core.windows.net/y9ne9ilzmfld/Stock.CSV
- It is separated by `;`
- The file has a size of 637 MB
- Number of records: 17175295

# Requirements
- Net Core 3.1 : https://dotnet.microsoft.com/download/dotnet/3.1
- SQL SERVER Local DB : https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15

# How to Run
- Configure your **appsettings.json** located in **CsvImporter.ConsoleApp**
	- **ConnectionStrings:StockDb** : Connection String of your database
	- **DataSource:Path** : Path of **.CSV** (Can be a local folder or URL from web)
	- **DataSource:IsUrl** : Specify if the file is obtained from web (true) or local (false)
- Set as StartUp Project and Use Package Manager Console Located in **CsvImporter.Core** and Run `Update-Database`
- Run **CsvImporter.ConsoleApp**

# Stock Entity
| PointOfSale | Product | Date | Stock |
|--|--|--|--|
| String | String | DateTime | Integer

# Performance and statistics in Debug

## Using Entity Framework Core

| Method | CPU Usage | Memory Usage | Average # Records Inserted per second | Time Elapsed
|--|--|--|--|--|
| BulkInsert (SUPER FAST) | 20% | 82MB | 40891 | 5 Minutes
| Add Range | 25% | 1GB | 500 | -
| Add (ULTRA SLOW) | 33% | 40MB | 6 | -

# Special Thanks
- Repo: https://github.com/borisdj/EFCore.BulkExtensions


