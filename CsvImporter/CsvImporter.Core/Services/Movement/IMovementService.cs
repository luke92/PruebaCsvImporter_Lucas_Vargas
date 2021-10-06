using CsvImporter.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvImporter.Core.Services.Movement
{
    public interface IMovementService
    {
        Task SaveAsync(StockMovement stockMovement);
        Task SaveAsync(IList<StockMovement> stockMovements, int? batchSize = null);
        Task Clear();
    }
}
