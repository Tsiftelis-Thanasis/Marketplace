using Marketplace.Models;
using MarketPlaceDTO;
using MarketPlaceServices.Interfaces;

namespace MarketplaceServices.Interfaces
{
    public interface IItemDtoService : IService<Item, ItemDto>
    {
    }
}