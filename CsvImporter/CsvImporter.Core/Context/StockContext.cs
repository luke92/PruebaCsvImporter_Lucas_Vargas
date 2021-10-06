using CsvImporter.Core.Domain;
using CsvImporter.Core.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CsvImporter.Core.Context
{
    public class StockContext : DbContext
    {
        public StockContext() 
        { 
        }

        public StockContext(DbContextOptions<StockContext> options) : base(options)
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StockDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StockMovementMap());
        }

        public virtual DbSet<StockMovement> StockMovements { get; set; }
    }
}
