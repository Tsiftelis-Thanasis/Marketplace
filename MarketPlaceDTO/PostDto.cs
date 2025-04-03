using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlaceDTO
{
    public class PostDto
    {
        [NotMapped]
        public int Id { get; set; }  // Required for editing/updating

        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; } // Store uploaded image URL
        public string Price { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; } // The user who posted the item
    }
}