using Marketplace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceUI.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task AddUserAsync(User user);
        Task EditUserAsync(int userId, User user);
        Task DeleteUserAsync(int userId);
    }

}
