namespace MarketPlaceServices.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string cacheKey);

        Task SetAsync<T>(string cacheKey, T value, TimeSpan? expiry = null);

        Task RemoveAsync(string cacheKey);
    }
}