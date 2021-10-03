using CsvImporter.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImporter.Core.Context
{
    public class StockContext : DbContext
    {
        public virtual DbSet<StockMovement> StockMovements { get; set; }
    }
}
