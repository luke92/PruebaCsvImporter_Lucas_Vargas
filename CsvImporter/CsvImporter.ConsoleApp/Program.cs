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
            var services = Startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            await serviceProvider.GetService<EntryPoint>().RunAsync(args);
        }
    }
}
