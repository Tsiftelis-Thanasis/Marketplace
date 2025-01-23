using MarketplaceServices.Interfaces;
using Marketplace.Models;
using MarketplaceRepository.Interfaces;
using MarketPlaceDTO;

namespace MarketplaceServices.Services
{
    public class ItemService : IItemService
    {

        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemDto> CreateItemAsync(ItemDto itemDto)
        {
            var item = new Item
            {
                Title = itemDto.Name,
                Price = itemDto.Price
            };

            await _itemRepository.AddAsync(item);

            return itemDto;
        }

        public async Task<ItemDto> GetItemByIdAsync(Guid itemId)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item == null)
                throw new Exception("Item not found");

            return new ItemDto
            {
                Id = item.ItemId,
                Name = item.Title,
                Price = item.Price
            };
        }
    }

}