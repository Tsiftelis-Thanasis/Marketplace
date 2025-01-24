using Marketplace.Models;
using Microsoft.EntityFrameworkCore;
using MarketplaceRepository.Interfaces;
using MarketplaceRepository.Data;
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
