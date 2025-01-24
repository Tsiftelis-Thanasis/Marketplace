using Marketplace.Models;
using MarketplaceRepository.Data;
using MarketplaceRepository.Interfaces;
using MarketPlaceRepository.Repositories;

namespace MarketplaceRepository.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly MarketplaceDbContext _context;

        public ItemRepository(MarketplaceDbContext context) : base(context)
        {
            _context = context;
        }
    }
}