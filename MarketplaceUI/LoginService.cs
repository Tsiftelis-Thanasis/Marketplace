using Marketplace.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MarketplaceUI
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(LoginModel loginModel);
    }

    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public LoginService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<bool> LoginAsync(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                await _localStorageService.SetItemAsync("authToken", token);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
