using AutoMapper;
using Marketplace.Models;
using MarketPlaceDTO;
using MarketplaceRepository.Interfaces;
using MarketplaceServices.Interfaces;
using MarketPlaceServices.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace MarketplaceServices.Services
{
    public class ItemDtoService : Service<Item, ItemDto>, IItemDtoService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public ItemDtoService(IItemRepository itemRepository, IMapper mapper, IDistributedCache cache) : base(itemRepository, mapper, cache)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _cache = cache;
        }
    }
}