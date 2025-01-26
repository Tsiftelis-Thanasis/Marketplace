using Marketplace.Models;
using System.Threading.Tasks;

namespace MarketplaceUI.Interfaces
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(LoginModel loginModel);
        Task<bool> RegisterAsync(RegisterModel registerModel);
    }

}
