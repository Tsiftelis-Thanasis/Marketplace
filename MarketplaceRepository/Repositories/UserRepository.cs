using Marketplace.Models;
using MarketplaceRepository.Data;
using MarketplaceRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceRepository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MarketplaceDbContext _context;

        public UserRepository(MarketplaceDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Items)
                .Include(u => u.Transactions)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> GetUserByUsernameOrEmailAsync(string username, string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username || u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _context.Users.AnyAsync(u => u.Username == username || u.Email == email);
        }
    }
}
