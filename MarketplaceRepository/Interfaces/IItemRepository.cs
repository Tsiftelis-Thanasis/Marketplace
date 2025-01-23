using Marketplace.Models;

namespace MarketplaceRepository.Interfaces
{
    public interface IItemRepository
    {
        Task AddAsync(Item item);
        Task<Item?> GetByIdAsync(Guid itemId);
    }
}
