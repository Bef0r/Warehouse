using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Model
{
    public class CabinetCreateModel
    {
        public int WarehouseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ShelfCreateModel> Shelves { get; set; } = new();
    }
}
