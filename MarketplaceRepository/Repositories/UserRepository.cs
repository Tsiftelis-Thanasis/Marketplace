using Marketplace.Models;
using MarketplaceRepository.Data;
using MarketplaceRepository.Interfaces;
using MarketPlaceRepository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceRepository.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly MarketplaceDbContext _context;

        public UserRepository(MarketplaceDbContext context): base(context) 
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameOrEmailAsync(string username, string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username || u.Email == email);
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _context.Users.AnyAsync(u => u.Username == username || u.Email == email);
        }
    }
}
