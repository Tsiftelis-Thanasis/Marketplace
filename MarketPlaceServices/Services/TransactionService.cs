using Marketplace.Models;
using MarketplaceAPI;
using MarketPlaceDTO;
using MarketPlaceModels.Enums;
using MarketplaceRepository.Interfaces;
using MarketplaceServices.Interfaces;

namespace MarketplaceServices.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionDto> CreateTransactionAsync(TransactionDto transactionDto)
        {
            var transaction = new Transaction
            {
                Amount = transactionDto.Amount,
                TransactionStatus = (Status)transactionDto.Status,
                ItemId = transactionDto.ItemId,
                CreatedDate = DateTime.UtcNow,
                BuyerId = transactionDto.BuyerId
            };

            await _transactionRepository.AddAsync(transaction);

            return transactionDto;
        }

        public async Task<TransactionDto> GetTransactionByIdAsync(Guid transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);
            if (transaction == null)
                throw new Exception("Transaction not found");

            return new TransactionDto
            {
                Id = transaction.TransactionId,
                Amount = transaction.Amount,
                Status = (int)transaction.TransactionStatus,
                ItemId = transaction.ItemId,
                CreatedDate = transaction.CreatedDate,
                BuyerId = transaction.BuyerId
            };
        }
    }

}