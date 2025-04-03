using Marketplace.Models;
using MarketplaceUI.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MarketplaceUI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _httpClient.GetFromJsonAsync<List<User>>("api/user");
            return users;
        }

        public async Task AddUserAsync(User user)
        {
            await _httpClient.PostAsJsonAsync("api/user", user);
        }

        public async Task EditUserAsync(int userId, User user)
        {
            await _httpClient.PutAsJsonAsync($"api/user/{userId}", user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _httpClient.DeleteAsync($"api/user/{userId}");
        }
    }
}