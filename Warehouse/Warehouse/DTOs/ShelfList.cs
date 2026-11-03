using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.DTOs
{
    public class ShelfList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasFreeSpace { get; set; }
    }
}
