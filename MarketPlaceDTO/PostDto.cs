namespace MarketPlaceDTO
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; } // The user who posted the item
    }
}