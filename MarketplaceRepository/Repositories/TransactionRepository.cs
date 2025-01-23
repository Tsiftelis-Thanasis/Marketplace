using Marketplace.Models;
using MarketplaceRepository.Data;
using MarketplaceRepository.Interfaces;

namespace MarketplaceRepository.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MarketplaceDbContext _context;

        public TransactionRepository(MarketplaceDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<Transaction?> GetByIdAsync(Guid transactionId)
        {
            return await _context.Transactions.FindAsync(transactionId);
        }
    }

}
