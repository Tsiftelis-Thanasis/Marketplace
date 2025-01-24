using MarketplaceServices.Interfaces;
using Marketplace.Models;
using MarketplaceRepository.Interfaces;
using MarketPlaceDTO;
using MarketPlaceServices.Services;
using AutoMapper;
using MarketPlaceRepository.Repositories;

namespace MarketplaceServices.Services
{
    public class ItemService : Service<Item, ItemDto>, IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)  : base(itemRepository, mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }
    }
}