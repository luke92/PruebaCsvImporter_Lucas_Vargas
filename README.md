# PruebaCsvImporter_Lucas_Vargas
Programa de consola .NET Core en C#, que lee un fichero .csv almacenado en una cuenta de almacenamiento de Azure e inserta su contenido en una BD SQL Server local.

# Data Source
- El fichero .csv está disponible en https://storage10082020.blob.core.windows.net/y9ne9ilzmfld/Stock.CSV
- Esta separado por ;
- El archivo pesa 637 MB

# Entidad Stock
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
