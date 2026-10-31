using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Entites
{
    public class Cabinet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;
        public ICollection<Shelf> Shelves { get; set; } = new List<Shelf>();
    }
}
