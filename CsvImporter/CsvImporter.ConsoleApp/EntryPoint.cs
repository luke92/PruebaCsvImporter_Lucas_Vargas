using CsvImporter.Core.Services.Importer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
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
            var path = "";
            var isFromWeb = false;
            var hasHeaderRecord = true;
            if (args.Length != 0 && args.Length != 3)
            {
                _logger.LogError("Need specify: [the path of .csv] [true or false if the path is from web] [true or false if the first record is the Header of CSV]");
            }
            else
            {
                if (args.Length == 0)
                {
                    path = _configuration.GetSection("DataSource:Path").Value;
                    bool.TryParse(_configuration.GetSection("DataSource:IsUrl").Value, out isFromWeb);
                    bool.TryParse(_configuration.GetSection("DataSource:HasHeaderRecord").Value, out hasHeaderRecord);
                }
                else
                {
                    path = args[0];
                    bool.TryParse(args[1], out isFromWeb);
                    bool.TryParse(args[2], out hasHeaderRecord);
                }
                _logger.LogInformation($"Start importing at {DateTime.Now}");
                await _importerService.ImportStockAsync(path, isFromWeb, hasHeaderRecord);
                _logger.LogInformation($"Finalize importing at {DateTime.Now}");
            }          
        }
    }
}
