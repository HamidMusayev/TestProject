namespace Api.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
    }
}
