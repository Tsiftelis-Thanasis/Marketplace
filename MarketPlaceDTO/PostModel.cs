using MarketPlaceModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace MarketPlaceDTO
{
    public class PostModel
    {
        public int PostId { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public Status PostStatus { get; set; } = Status.Pending;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; } // Set automatically
    }
}