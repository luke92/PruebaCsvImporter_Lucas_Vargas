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
            var filepath = _configuration.GetSection("dataSource:uri").Value;
            var isFromWeb = false;
            var hasHeaderRecord = true;
            bool.TryParse(_configuration.GetSection("dataSource:isUrl").Value, out isFromWeb);
            bool.TryParse(_configuration.GetSection("dataSource:hasHeaderRecord").Value, out hasHeaderRecord);
            await _importerService.ImportStockAsync(filepath, isFromWeb, hasHeaderRecord);
            _logger.LogInformation($"Finalize importing at {DateTime.Now}");
        }
    }
}
