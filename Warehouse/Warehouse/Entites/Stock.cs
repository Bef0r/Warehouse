using Warehouse.Entites;

namespace Warehouse.Entities;

public class Stock
{
    public int Id { get; set; }
    public int ShelfId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int PositionZ { get; set; }
    public Shelf Shelf { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
