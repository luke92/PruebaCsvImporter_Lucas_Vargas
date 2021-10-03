using CsvImporter.Core.Context;
using CsvImporter.Core.Domain;
using CsvImporter.Core.Mapping;
using CsvImporter.Core.Services.Importer;
using CsvImporter.Core.Services.Movement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CsvImporter.ConsoleApp
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var collection = new ServiceCollection();

            var connectionString = configuration.GetSection("connectionString").Value;
            collection.AddDbContext<StockContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            collection.AddScoped<IMovementService, MovementService>();
            collection.AddScoped<IImporterService, CsvImporterService>();

            var serviceProvider = collection.BuildServiceProvider();
            
            Console.WriteLine("Start importing");

            var dataSource = configuration.GetSection("dataSource").Value;
            var importerService = serviceProvider.GetService<IImporterService>();
            await importerService.ImportStock(dataSource);

            Console.WriteLine("End importing");

            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}
