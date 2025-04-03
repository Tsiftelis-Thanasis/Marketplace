using Marketplace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceUI.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync();

        Task<Post> GetPostByIdAsync(int id);

        Task<Post> CreatePostAsync(Post post);

        Task<Post> UpdatePostAsync(Post post);

        Task<bool> DeletePostAsync(int id);
    }
}