using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Entites
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public ICollection<Cabinet> Cabinets { get; set; } = new List<Cabinet>();
    }
}
