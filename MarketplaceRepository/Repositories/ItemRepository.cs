using Marketplace.Models;
using MarketplaceRepository.Data;
using MarketplaceRepository.Interfaces;

namespace MarketplaceRepository.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly MarketplaceDbContext _context;

        public ItemRepository(MarketplaceDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Item?> GetByIdAsync(Guid itemId)
        {
            return await _context.Items.FindAsync(itemId);
        }
    }
}
