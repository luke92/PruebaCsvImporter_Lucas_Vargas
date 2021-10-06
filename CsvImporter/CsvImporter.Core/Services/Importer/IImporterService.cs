﻿using CsvHelper.Configuration;
using CsvImporter.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CsvImporter.Core.Services.Importer
{
    public interface IImporterService
    {
        Task ImportStockAsync(string filePath, bool isUrl = false, bool hasHeaderRecord = true);
    }
}
