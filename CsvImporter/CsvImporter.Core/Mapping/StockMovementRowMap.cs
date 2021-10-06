using CsvHelper.Configuration;
using CsvImporter.Core.Domain;

namespace CsvImporter.Core.Mapping
{
    public class StockMovementRowMap : ClassMap<StockMovement>
    {
        public StockMovementRowMap()
        {
            Map(x => x.PointOfSale).Index(0);
            Map(x => x.Product).Index(1);
            Map(x => x.Date).Index(2);
            Map(x => x.Stock).Index(3);
        }
    }
}
