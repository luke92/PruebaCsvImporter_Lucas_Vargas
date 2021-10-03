using CsvImporter.Core.Context;
using CsvImporter.Core.Domain;
using CsvImporter.Core.Mapping;
using CsvImporter.Core.Services.Importer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CsvImporter.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var collection = new ServiceCollection();

            collection.AddScoped<IImporterService, CsvImporterService>();

            var connectionString = Config.GetSection("connectionString").Value;
            collection.AddDbContext<StockContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            IServiceProvider serviceProvider = collection.BuildServiceProvider();
            var sample1Service = serviceProvider.GetService<IImporterService>();
            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }

            Console.WriteLine("Start importing");
            var service = new CsvImporterService();

            var dataSource = Config.GetSection("dataSource").Value;

            service.Read<StockMovement,StockMovementRowMap>(dataSource);
            Console.WriteLine("End importing");
        }
    }
}
