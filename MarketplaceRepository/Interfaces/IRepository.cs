namespace MarketPlaceRepository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}