using Marketplace.Models;
using MarketplaceUI.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MarketplaceUI.Services
{
  
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
            var response = await _httpClient.PostAsJsonAsync("api/User/login", loginModel);

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


        public async Task<bool> RegisterAsync(RegisterModel registerModel) // New method
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/register", registerModel);

            return response.IsSuccessStatusCode;
        }

    }

}
