using Marketplace.Models;
using Microsoft.EntityFrameworkCore;
using MarketplaceRepository.Interfaces;
using MarketplaceRepository.Data;

namespace MarketplaceRepository.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MarketplaceDbContext _context;

        public PostRepository(MarketplaceDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<Post?> GetByIdAsync(int postId)
        {
            return await _context.Posts.FindAsync(postId);
        }


        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync();
        }

       

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

    }

}
