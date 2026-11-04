using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.DTOs
{
    public class StockDetails
    {
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double PositionZ { get; set; }
    }
}
