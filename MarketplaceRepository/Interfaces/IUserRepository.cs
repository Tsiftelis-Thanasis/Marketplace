using Marketplace.Models;

namespace MarketplaceRepository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int id);

        Task<User?> GetUserByUsernameOrEmailAsync(string username, string email);

        Task AddUserAsync(User user);

        Task<bool> UserExistsAsync(string username, string email);
    }
}