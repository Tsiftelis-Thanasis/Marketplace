using Marketplace.Models;
using MarketplaceRepository.Data;
using MarketplaceRepository.Interfaces;
using MarketPlaceRepository.Repositories;

namespace MarketplaceRepository.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly MarketplaceDbContext _context;

        public PostRepository(MarketplaceDbContext context) : base(context)
        {
            _context = context;
        }
    }
}