using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Entities;

namespace Warehouse.Entites
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? FromShelfId { get; set; }
        public int? ToShelfId { get; set; }
        public int Quantity { get; set; }
        public MovementType MovementType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Note { get; set; }
        public Product Product { get; set; } = null!;
        public Shelf? FromShelf { get; set; }
        public Shelf? ToShelf { get; set; }
    }
}
