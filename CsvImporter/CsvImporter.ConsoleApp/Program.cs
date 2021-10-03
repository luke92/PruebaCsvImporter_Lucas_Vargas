using CsvImporter.Core.Domain;
using CsvImporter.Core.Mapping;
using CsvImporter.Core.Services.Importer;
using System;

namespace CsvImporter.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start importing");
            var service = new CsvImporterService();
            service.Read<StockMovement,StockMovementRowMap>(@"C:\Users\Luk3\source\repos\Stock.csv");
            Console.WriteLine("End importing");
        }
    }
}
