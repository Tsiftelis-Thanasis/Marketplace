namespace Marketplace.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; } // Foreign key for User

        public User? User { get; set; } // Navigation property
    }
}
