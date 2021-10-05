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

namespace CsvImporter.Core.Services.Importer
{
    public class CsvImporterService : IImporterService
    {
        private readonly IMovementService _movementService;
        private readonly ILogger _logger;

        public CsvImporterService(IMovementService movementService, ILogger<CsvImporterService> logger)
        {
            _movementService = movementService;
            _logger = logger;
        }

        public async Task ImportStock(string filePath, string delimiter = null)
        {
            _logger.LogInformation($"Start importing at {DateTime.Now}");
            using (var reader = new StreamReader(filePath))
            {
                if(delimiter == null)
                    delimiter = DetectDelimiter(reader);

                var config = new CsvConfiguration(CultureInfo.CurrentCulture) 
                { 
                    Delimiter = delimiter, 
                    Encoding = Encoding.UTF8,
                    HasHeaderRecord = true                    
                };

                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<StockMovementRowMap>();
                    csv.Read();
                    csv.ReadHeader();
                    await _movementService.Clear();
                    while (csv.Read())
                    {
                        var record = csv.GetRecord<StockMovement>();
                        await _movementService.Save(record);
                    }
                }
            }
            _logger.LogInformation($"Finalize importing at {DateTime.Now}");
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
