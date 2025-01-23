using Marketplace.Models;

namespace MarketplaceRepository.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task<Transaction?> GetByIdAsync(Guid transactionId);
    }
}
