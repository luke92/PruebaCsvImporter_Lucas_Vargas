using CsvImporter.Core.Context;
using CsvImporter.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

namespace CsvImporter.Core.Services.Movement
{
    public class MovementService : IMovementService
    {
        private readonly StockContext _db;
        private readonly int _defaultBatchSize;

        public MovementService(StockContext db)
        {
            _db = db;
            _defaultBatchSize = 100000;
        }

        public async Task SaveAsync(IList<StockMovement> stockMovements, int? batchSize = null)
        {
            await _db.BulkInsertAsync(stockMovements, new BulkConfig
            {
                BatchSize = batchSize.HasValue ? batchSize.Value : _defaultBatchSize
            });
        }

        public async Task SaveAsync(StockMovement stockMovement)
        {
            await _db.StockMovements.AddAsync(stockMovement);
            await _db.SaveChangesAsync();
        }

        public async Task Clear()
        {
            await _db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [StockMovements]");
            await _db.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT('StockMovements', RESEED, 1)");
        }
    }
}
