using MarketplaceServices.Interfaces;
using Marketplace.Models;
using MarketplaceRepository.Interfaces;
using MarketPlaceDTO;
using MarketPlaceServices.Services;
using AutoMapper;
using MarketPlaceRepository.Repositories;
using MarketPlaceServices.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace MarketplaceServices.Services
{
    public class ItemService : Service<Item, ItemDto>, IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        public ItemService(IItemRepository itemRepository, IMapper mapper, IDistributedCache cache)  : base(itemRepository, mapper, cache)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _cache = cache;
        }
    }
}