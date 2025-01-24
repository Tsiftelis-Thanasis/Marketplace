using AutoMapper;
using Marketplace.Models;
using MarketplaceAPI;
using MarketPlaceDTO;
using MarketPlaceModels.Enums;
using MarketplaceRepository.Interfaces;
using MarketplaceRepository.Repositories;
using MarketplaceServices.Interfaces;
using MarketPlaceServices.Services;

namespace MarketplaceServices.Services
{
    public class TransactionService : Service<Transaction, TransactionDto>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper) : base(transactionRepository, mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

    }

}