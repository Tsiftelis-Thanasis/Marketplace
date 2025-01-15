using System;

namespace MarketplaceAPI.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int ItemId { get; set; }
        public string Status { get; set; } = "Pending"; // Default status
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public User? Buyer { get; set; }
        public User? Seller { get; set; }
        public Item? Item { get; set; }
    }
}
