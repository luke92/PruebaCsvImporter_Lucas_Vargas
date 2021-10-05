using CsvImporter.Core.Context;
using CsvImporter.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CsvImporter.Core.Services.Movement
{
    public class MovementService : IMovementService
    {
        private readonly StockContext _db;

        public MovementService(StockContext db)
        {
            _db = db;
        }

        public async Task SaveAsync(IList<StockMovement> stockMovements)
        {
            await _db.StockMovements.AddRangeAsync(stockMovements);
            await _db.SaveChangesAsync();
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
