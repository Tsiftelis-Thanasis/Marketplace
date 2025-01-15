using System;

namespace MarketplaceAPI.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Status { get; set; } = "Pending"; // Default status
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public User? User { get; set; }
    }
}
