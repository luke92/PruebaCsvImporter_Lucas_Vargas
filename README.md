# PruebaCsvImporter_Lucas_Vargas
Programa de consola .NET Core en C#, que lee un fichero .csv almacenado en una cuenta de almacenamiento de Azure e inserta su contenido en una BD SQL Server local.

# Origen de datos
- El fichero .csv está disponible en https://storage10082020.blob.core.windows.net/y9ne9ilzmfld/Stock.CSV
- Esta separado por ;

# Entidad Stock
| PointOfSale | Product | Date | Stock |
|--|--|--|--|
| Integer | String | DateTime | Integer