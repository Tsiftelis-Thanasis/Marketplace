using Marketplace.Models;
using MarketplaceRepository.Data;
using MarketplaceRepository.Interfaces;
using MarketPlaceRepository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceRepository.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private readonly MarketplaceDbContext _context;

        public TransactionRepository(MarketplaceDbContext context) : base(context)
        {
            _context = context;
        }
        
    }

}
