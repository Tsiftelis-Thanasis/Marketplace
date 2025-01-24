using Marketplace.Models;
using MarketPlaceDTO;
using MarketPlaceServices.Interfaces;

namespace MarketplaceServices.Interfaces
{   
    public interface IItemService : IService<Item, ItemDto>
    {
    }

}