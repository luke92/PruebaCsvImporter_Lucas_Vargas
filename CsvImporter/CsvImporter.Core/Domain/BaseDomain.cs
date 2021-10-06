using System;

namespace CsvImporter.Core.Domain
{
    public class BaseDomain
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public BaseDomain()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
