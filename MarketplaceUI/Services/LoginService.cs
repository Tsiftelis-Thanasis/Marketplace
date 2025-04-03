using Marketplace.Models;
using MarketPlaceModels.Models;
using MarketplaceUI.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MarketplaceUI.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthStateProvider _authStateProvider;

        public LoginService(HttpClient httpClient, CustomAuthStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
        }

        public async Task<string?> LoginAsync(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/login", loginModel);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result is null || string.IsNullOrEmpty(result.Token))
            {
                return null;
            }

            await _authStateProvider.MarkUserAsAuthenticated(result.Token);
            return result.Token;
        }

        public async Task<bool> RegisterAsync(RegisterModel registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/register", registerModel);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            //var error = await response.Content.ReadAsStringAsync();
            return false;
        }
    }
}