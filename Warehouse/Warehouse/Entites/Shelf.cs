using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Entities;

namespace Warehouse.Entites
{
    public class Shelf
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CabinetId { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public Cabinet Cabinet { get; set; } = null!;
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
        public ICollection<StockMovement> OutgoingMovements { get; set; } = new List<StockMovement>();
        public ICollection<StockMovement> IncomingMovements { get; set; } = new List<StockMovement>();
    }
}
