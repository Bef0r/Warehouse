using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Model
{
    public class ShelfCreateModel
    {
        public string Name { get; set; } = string.Empty;
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
    }
}
