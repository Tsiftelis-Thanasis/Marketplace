using Marketplace.Models;

namespace MarketplaceRepository.Interfaces
{
    public interface IPostRepository
    {
        Task AddAsync(Post post);
        Task<Post?> GetByIdAsync(int postId);

        Task<IEnumerable<Post>> GetAllAsync();
       
        Task UpdateAsync(Post post);
        Task DeleteAsync(Post post);

    }
}
