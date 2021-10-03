using CsvImporter.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CsvImporter.Core.Services.Movement
{
    public interface IMovementService
    {
        Task Save(StockMovement stockMovement);
        Task Clear();
    }
}
