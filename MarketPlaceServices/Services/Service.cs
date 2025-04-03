using AutoMapper;
using MarketPlaceRepository.Interfaces;
using MarketPlaceServices.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace MarketPlaceServices.Services
{
    public class Service<TEntity, TDto> : IService<TEntity, TDto>
    where TEntity : class
    where TDto : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        public Service(IRepository<TEntity> repository, IMapper mapper, IDistributedCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);

            await _cache.RemoveAsync($"GetAll_{typeof(TEntity).Name}");

            return _mapper.Map<TDto>(entity);
        }

        private async Task<T> GetFromCacheOrSource<T>(string cacheKey, Func<Task<T>> sourceFunc)
        {
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonConvert.DeserializeObject<T>(cachedData);
            }

            var result = await sourceFunc();
            if (result != null)
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _cacheDuration
                };
                await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result), options);
            }

            return result;
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            string cacheKey = $"{typeof(TEntity).Name}_{id}";
            return await GetFromCacheOrSource(cacheKey, async () =>
            {
                var entity = await _repository.GetByIdAsync(id);
                return _mapper.Map<TDto>(entity);
            });
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            string cacheKey = $"GetAll_{typeof(TEntity).Name}";
            return await GetFromCacheOrSource(cacheKey, async () =>
            {
                var entities = await _repository.GetAllAsync();
                return _mapper.Map<IEnumerable<TDto>>(entities);
            });
        }

        public async Task<bool> UpdateAsync(int id, TDto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
                return false;

            var updatedEntity = _mapper.Map(dto, existingEntity);
            await _repository.UpdateAsync(updatedEntity);

            // Invalidate cache
            await _cache.RemoveAsync($"{typeof(TEntity).Name}_{id}");
            await _cache.RemoveAsync($"GetAll_{typeof(TEntity).Name}");

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _repository.DeleteAsync(entity);

            // Invalidate cache
            await _cache.RemoveAsync($"{typeof(TEntity).Name}_{id}");
            await _cache.RemoveAsync($"GetAll_{typeof(TEntity).Name}");

            return true;
        }
    }
}