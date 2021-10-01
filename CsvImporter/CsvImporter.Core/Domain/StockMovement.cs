﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImporter.Core.Domain
{
    public class StockMovement
    {
        public int PointOfSale { get; set; }
        public string Product { get; set; }
        public DateTime Date { get; set; }
        public short Stock { get; set; }
    }
}