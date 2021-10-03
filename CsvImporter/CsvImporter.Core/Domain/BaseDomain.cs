using System;
using System.Collections.Generic;
using System.Text;

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
