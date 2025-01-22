using MarketplaceAPI.Models;

namespace MarketplaceAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameOrEmailAsync(string username, string email);
        Task<User> RegisterUserAsync(User user);
        Task<bool> UserExistsAsync(string username, string email);
        string GenerateJwtToken(User user);
    }
}