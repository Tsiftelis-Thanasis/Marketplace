using MarketPlaceModels.Enums;

namespace Marketplace.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Roles Role { get; set; } = Roles.User;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}