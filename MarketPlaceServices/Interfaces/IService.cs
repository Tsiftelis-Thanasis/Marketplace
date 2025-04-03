namespace MarketPlaceServices.Interfaces
{
    public interface IService<TEntity, TDto>
    {
        Task<TDto> CreateAsync(TDto dto);

        Task<TDto> GetByIdAsync(int id);

        Task<IEnumerable<TDto>> GetAllAsync();

        Task<bool> UpdateAsync(int id, TDto dto);

        Task<bool> DeleteAsync(int id);
    }
}