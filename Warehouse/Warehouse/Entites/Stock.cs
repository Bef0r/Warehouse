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
    public virtual Shelf Shelf { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}
