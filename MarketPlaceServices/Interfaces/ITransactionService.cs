using MarketPlaceDTO;

namespace MarketplaceServices.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionDto> CreateTransactionAsync(TransactionDto transactionDto);
        Task<TransactionDto> GetTransactionByIdAsync(Guid transactionId);
           
    }
}
