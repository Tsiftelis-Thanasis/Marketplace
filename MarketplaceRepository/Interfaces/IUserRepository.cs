using Marketplace.Models;
using MarketPlaceRepository.Interfaces;

namespace MarketplaceRepository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByUsernameOrEmailAsync(string username, string email);

        Task<bool> UserExistsAsync(string username, string email);
    }
}