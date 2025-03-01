using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Marketplace.Models;
using MarketplaceServices.Interfaces;
using MarketPlaceServices.Interfaces;

namespace MarketplaceServices.Services
{

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        private readonly ICacheService _cacheService;

        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);


        public UserService(HttpClient httpClient, ICacheService cacheService)
        {
            _httpClient = httpClient;
            _cacheService = cacheService;
        }

        public async Task<List<User>> GetUsersAsync()
        {

            const string cacheKey = "GetAllUsers";
            var cachedUsers = await _cacheService.GetAsync<List<User>>(cacheKey);
            if (cachedUsers != null) return cachedUsers;

            var users = await _httpClient.GetFromJsonAsync<List<User>>("api/user");
            await _cacheService.SetAsync(cacheKey, users, _cacheDuration);
            return users;

        }

        public async Task AddUserAsync(User user)
        {
            await _httpClient.PostAsJsonAsync("api/user", user);
            await _cacheService.RemoveAsync("GetAllUsers");

        }

        public async Task EditUserAsync(int userId, User user)
        {
            await _httpClient.PutAsJsonAsync($"api/user/{userId}", user);
            await _cacheService.RemoveAsync("GetAllUsers");
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _httpClient.DeleteAsync($"api/user/{userId}");
            await _cacheService.RemoveAsync("GetAllUsers");
        }
    }

}
