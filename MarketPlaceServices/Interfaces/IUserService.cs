using Marketplace.Models;
using MarketPlaceDTO;
using MarketPlaceServices.Interfaces;

namespace MarketplaceAPI.Services.Interfaces
{
    public interface IUserService : IService<User, UserDto>
    {
        Task<UserDto?> GetUserByUsernameOrEmailAsync(string username, string email);
        Task<UserDto> RegisterUserAsync(UserDto user);
        Task<bool> UserExistsAsync(string username, string email);
        string GenerateUserJwtToken(UserDto user);
    }
}