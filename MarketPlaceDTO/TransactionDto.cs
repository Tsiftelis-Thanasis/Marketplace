namespace MarketPlaceDTO
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public int ItemId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BuyerId { get; set; }
    }

}
