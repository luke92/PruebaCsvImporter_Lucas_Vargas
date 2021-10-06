using CsvHelper;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using CsvImporter.Core.Domain;
using CsvHelper.Configuration;
using CsvImporter.Core.Mapping;
using CsvImporter.Core.Services.Movement;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Configuration;
using CsvImporter.Core.Services.FileManager;

namespace CsvImporter.Core.Services.Importer
{
    public class CsvImporterService : IImporterService
    {
        private readonly IMovementService _movementService;
        private readonly ILogger _logger;
        private readonly IFileManagerService _fileManagerService;
        private readonly int _batchSize = 100000;

        public CsvImporterService(IMovementService movementService, ILogger<CsvImporterService> logger, IConfiguration configuration, IFileManagerService fileManagerService)
        {
            _movementService = movementService;
            _logger = logger;
            _fileManagerService = fileManagerService;

            if(configuration != null)
            {
                int.TryParse(configuration.GetSection("BatchSize").Value, out _batchSize);
            }
        }

        public async Task ImportStockAsync(string filePath, bool isUrl = false, bool hasHeaderRecord = true)
        {
            var reader = _fileManagerService.StreamReader(filePath, isUrl);
            if (reader == null) 
            {
                _logger.LogError($"File {filePath} doesn't exists");
                return;
            }

            var config = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Encoding = Encoding.UTF8,
                HasHeaderRecord = hasHeaderRecord,
                DetectDelimiter = true
            };

            var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<StockMovementRowMap>();
            
            await _movementService.Clear();

            var listRecords = new List<StockMovement>();
            var recordsImported = (long)0;
            var recordsNotImported = (long)0;

            while (csv.Read())
            {
                try
                {
                    var record = csv.GetRecord<StockMovement>();
                    if(record != null)
                        listRecords.Add(record);
                }
                catch(Exception ex)
                {
                    recordsNotImported++;
                    _logger.LogError($"Cannot convert record", ex);
                }

                if (listRecords.Count == _batchSize)
                {
                    recordsImported += await SaveRecords(listRecords);
                }                
            }

            recordsImported += await SaveRecords(listRecords);

            LogResults(recordsImported, recordsNotImported);
        }

        private async Task<long> SaveRecords(IList<StockMovement> listRecords)
        {
            await _movementService.SaveAsync(listRecords, _batchSize);
            var recordsImported = listRecords.Count;
            listRecords.Clear();
            return recordsImported;
        }

        private void LogResults(long recordsImported, long recordsCannotImported)
        {
            _logger.LogInformation($"Records Imported: {recordsImported}");
            _logger.LogInformation($"Records Not Imported: {recordsCannotImported}");
        }
    }
}
