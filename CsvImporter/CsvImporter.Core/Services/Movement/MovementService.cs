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

        public async Task Save(StockMovement stockMovement)
        {
            _db.StockMovements.Add(stockMovement);
            await _db.SaveChangesAsync();
        }

        public async Task Clear()
        {
            await _db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [StockMovements]");
        }
    }
}
