using CsvHelper;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using CsvImporter.Core.Domain;
using CsvHelper.Configuration;
using CsvImporter.Core.Mapping;
using CsvImporter.Core.Services.Movement;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Configuration;

namespace CsvImporter.Core.Services.Importer
{
    public class CsvImporterService : IImporterService
    {
        private readonly IMovementService _movementService;
        private readonly ILogger _logger;
        private readonly int _batchSize = 100000;

        public CsvImporterService(IMovementService movementService, ILogger<CsvImporterService> logger, IConfiguration configuration)
        {
            _movementService = movementService;
            _logger = logger;

            if(configuration != null)
            {
                int.TryParse(configuration.GetSection("BatchSize").Value, out _batchSize);
            }
        }

        public async Task ImportStockAsync(string filePath, string delimiter = null)
        {            
            var reader = new StreamReader(filePath);
            if (reader == null) 
            {
                _logger.LogError($"File {filePath} doesn't exists");
                return;
            } 

            if (delimiter == null)
                delimiter = DetectDelimiter(reader);

            var config = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = delimiter,
                Encoding = Encoding.UTF8,
                HasHeaderRecord = true
            };

            if(config == null)
            {
                _logger.LogError($"Error initializing Configuration for CSVREADER");
                return;
            }
            
            var csv = new CsvReader(reader, config);
            if (csv == null)
            {
                _logger.LogError($"Error initializing CsvReader");
                return;
            }

            csv.Context.RegisterClassMap<StockMovementRowMap>();
            csv.Read();
            csv.ReadHeader();
            await _movementService.Clear();

            var listRecords = new List<StockMovement>();
            
            while (csv.Read())
            {
                listRecords.Add(csv.GetRecord<StockMovement>());
                if(listRecords.Count == _batchSize)
                {
                    await _movementService.SaveAsync(listRecords, _batchSize);
                    listRecords.Clear();
                }
            }
            await _movementService.SaveAsync(listRecords);            
        }

        private string DetectDelimiter(StreamReader reader)
        {
            var possibleDelimiters = new List<string> { ",", ";", "\t", "|" };

            var headerLine = reader.ReadLine();

            reader.BaseStream.Position = 0;
            reader.DiscardBufferedData();

            foreach (var possibleDelimiter in possibleDelimiters)
            {
                if (headerLine.Contains(possibleDelimiter))
                {
                    return possibleDelimiter;
                }
            }

            return possibleDelimiters[0];
        }
    }
}
