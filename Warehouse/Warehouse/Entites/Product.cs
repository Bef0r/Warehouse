using Warehouse.Entites;

namespace Warehouse.Entities;

public class Product
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public int MinimumStock { get; set; }
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<Stock> Stocks { get; set; }    = new List<Stock>();
}
