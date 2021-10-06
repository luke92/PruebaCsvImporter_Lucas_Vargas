using CsvImporter.Core.Services.FileManager;
using CsvImporter.Core.Services.Importer;
using CsvImporter.Core.Services.Movement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsvImporter.Tests.Core.Services
{
    public class ImporterServiceTests
    {
        IImporterService _importerService;
        Mock<IMovementService> _movementService;
        Mock<ILogger<CsvImporterService>> _logger;
        Mock<IFileManagerService> _fileManagerService;
        string filePath = "prueba.csv";
        byte[] fakeFileBytes;
        MemoryStream fakeMemoryStream;

        [SetUp]
        public void Setup()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"BatchSize", "10000"},
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            fakeFileBytes = Encoding.UTF8.GetBytes(filePath);
            fakeMemoryStream = new MemoryStream(fakeFileBytes);

            _movementService = new Mock<IMovementService>();
            _logger = new Mock<ILogger<CsvImporterService>>();
            _fileManagerService = new Mock<IFileManagerService>();
            _importerService = new CsvImporterService(_movementService.Object, _logger.Object, configuration, _fileManagerService.Object);
        }

        [Test]
        public void FileNotFoundError()
        {
            var error = "doesn't exists";
            _importerService.ImportStockAsync(filePath);
            Assert.IsTrue(_logger.Invocations[0].Arguments[2].ToString().Contains(error));
        }

        [Test]
        public void ImportStockWithoutErrorsWithZeroInserted()
        {
            _fileManagerService.Setup(x => x.StreamReader(It.IsAny<string>())).Returns(new StreamReader(fakeMemoryStream));            
            _importerService.ImportStockAsync(filePath);
            Assert.IsTrue(_logger.Invocations[0].Arguments[2].ToString().Contains("Records Imported: 0"));
        }

        [Test]
        public void ImportStockWithoutErrorsWithOneInsert()
        {
            var text = "PointOfSale;Product;Date;Stock" + "\r\n" + "121017;17240503103734;2019-08-17;2";
            var bytes = Encoding.UTF8.GetBytes(text);
            var stream = new MemoryStream(bytes);
            _fileManagerService.Setup(x => x.StreamReader(It.IsAny<string>())).Returns(new StreamReader(stream));
            _importerService.ImportStockAsync(filePath);
            Assert.IsTrue(_logger.Invocations[0].Arguments[2].ToString().Contains("Records Imported: 1"));
        }

        [Test]
        public void ImportStockWithOneError()
        {
            var text = "PointOfSale;Product;Date;Stock" + "\r\n" + "121017;17240503103734;2019-08-17;nosoyunnumero";
            var bytes = Encoding.UTF8.GetBytes(text);
            var stream = new MemoryStream(bytes);
            _fileManagerService.Setup(x => x.StreamReader(It.IsAny<string>())).Returns(new StreamReader(stream));
            _importerService.ImportStockAsync(filePath);
            Assert.IsTrue(_logger.Invocations[2].Arguments[2].ToString().Contains("Records Not Imported: 1"));
        }

    }
}
