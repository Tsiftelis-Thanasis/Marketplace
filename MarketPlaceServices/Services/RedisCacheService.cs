using MarketPlaceServices.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MarketPlaceServices.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetAsync<T>(string cacheKey)
        {
            var cachedData = await _cache.GetStringAsync(cacheKey);
            return cachedData == null ? default : JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task SetAsync<T>(string cacheKey, T value, TimeSpan? expiry = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(60)
            };
            var serializedData = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(cacheKey, serializedData, options);
        }

        public async Task RemoveAsync(string cacheKey)
        {
            await _cache.RemoveAsync(cacheKey);
        }
    }
}