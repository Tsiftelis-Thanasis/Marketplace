using MarketPlaceModels.Enums;

namespace Marketplace.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status PostStatus { get; set; } = Status.Pending;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; } // Foreign key for User
        public User? User { get; set; }
        public Item? Item { get; set; }
    }
}
