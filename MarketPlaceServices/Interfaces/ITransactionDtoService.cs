using Marketplace.Models;
using MarketPlaceDTO;
using MarketPlaceServices.Interfaces;

namespace MarketplaceServices.Interfaces
{
    public interface ITransactionDtoService : IService<Transaction, TransactionDto>
    {
    }
}
