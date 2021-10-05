using CsvImporter.Core.Context;
using CsvImporter.Core.Services.Importer;
using CsvImporter.Core.Services.Movement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsvImporter.ConsoleApp
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

            IConfiguration configuration = builder.Build();
            services.AddSingleton(configuration);

            services.AddDbContext<StockContext>(options =>
            {
                 options.UseSqlServer(configuration.GetConnectionString("StockDb"));                
            });

            services.AddSingleton<IMovementService, MovementService>();
            services.AddSingleton<IImporterService, CsvImporterService>();

            services.AddLogging(configure =>
            {
                configure.AddConfiguration(configuration.GetSection("Logging"));
                configure.AddConsole();
            });

            services.AddTransient<EntryPoint>();
            
            return services;
        }
    }
}
