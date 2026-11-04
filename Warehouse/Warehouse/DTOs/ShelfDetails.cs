namespace Warehouse.DTOs
{
    public class ShelfDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public List<StockDetails> Stocks { get; set; } = new();
    }
}
