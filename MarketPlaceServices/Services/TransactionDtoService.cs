using AutoMapper;
using Marketplace.Models;
using MarketPlaceDTO;
using MarketPlaceModels.Enums;
using MarketplaceRepository.Interfaces;
using MarketplaceRepository.Repositories;
using MarketplaceServices.Interfaces;
using MarketPlaceServices.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace MarketplaceServices.Services
{
    public class TransactionDtoService : Service<Transaction, TransactionDto>, ITransactionDtoService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;


        public TransactionDtoService(ITransactionRepository transactionRepository, IMapper mapper, IDistributedCache  cache) : 
            base(transactionRepository, mapper, cache)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _cache = cache;
        }

    }

}