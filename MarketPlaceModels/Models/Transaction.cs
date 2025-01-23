using MarketPlaceModels.Enums;
using System;

namespace Marketplace.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int ItemId { get; set; }
        public decimal Amount { get; set; }
        public Status TransactionStatus { get; set; } = Status.Pending;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public User? Buyer { get; set; }
        public User? Seller { get; set; }
        public Item? Item { get; set; }
    }
}
