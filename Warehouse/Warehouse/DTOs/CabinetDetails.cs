namespace Warehouse.DTOs
{
    public class CabinetDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ShelfDetails> Shelves { get; set; } = new();
    }
}
