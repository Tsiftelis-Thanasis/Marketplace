using System;
using System.Collections.Generic;

namespace Marketplace.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Buyer"; // Default role
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ICollection<Item>? Items { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
