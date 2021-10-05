using CsvImporter.Core.Services.Importer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CsvImporter.ConsoleApp
{
    public class EntryPoint
    {
        private readonly IConfiguration _configuration;
        private readonly IImporterService _importerService;
        private readonly ILogger _logger;

        public EntryPoint(IConfiguration configuration, IImporterService importerService, ILogger<EntryPoint> logger)
        {
            _configuration = configuration;
            _importerService = importerService;
            _logger = logger;
        }

        public async Task RunAsync(String[] args)
        {
            _logger.LogInformation($"Start importing at {DateTime.Now}");
            var dataSource = _configuration.GetSection("dataSource").Value;
            await _importerService.ImportStockAsync(dataSource);
            _logger.LogInformation($"Finalize importing at {DateTime.Now}");
        }
    }
}
