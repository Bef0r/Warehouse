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
        public virtual Warehouse Warehouse { get; set; } = null!;
        public virtual ICollection<Shelf> Shelves { get; set; } = new List<Shelf>();
    }
}
