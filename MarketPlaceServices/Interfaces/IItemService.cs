using MarketPlaceDTO;

namespace MarketplaceServices.Interfaces
{
    public interface IItemService
    {
        Task<ItemDto> CreateItemAsync(ItemDto itemDto);

        Task<ItemDto> GetItemByIdAsync(Guid itemId);
    }
}