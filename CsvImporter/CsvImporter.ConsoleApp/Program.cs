using CsvImporter.Core.Context;
using CsvImporter.Core.Domain;
using CsvImporter.Core.Mapping;
using CsvImporter.Core.Services.Importer;
using CsvImporter.Core.Services.Movement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
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

            collection.AddLogging(configure =>
            {
                configure.AddConfiguration(configuration.GetSection("Logging"));
                configure.AddConsole();
            }); 
            collection.AddScoped<IMovementService, MovementService>();
            collection.AddScoped<IImporterService, CsvImporterService>();

            var serviceProvider = collection.BuildServiceProvider();
           
            var dataSource = configuration.GetSection("dataSource").Value;
            var importerService = serviceProvider.GetService<IImporterService>();
            await importerService.ImportStock(dataSource);

            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}
