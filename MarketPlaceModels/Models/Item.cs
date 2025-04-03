namespace Marketplace.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        //public string Status { get; set; } =
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}