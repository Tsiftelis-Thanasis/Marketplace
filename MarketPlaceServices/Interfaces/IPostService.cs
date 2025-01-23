using MarketPlaceDTO;

namespace MarketplaceServices.Interfaces
{
    public interface IPostService
    {
        Task<PostDto> CreatePostAsync(PostDto postDto);

        Task<PostDto> GetPostByIdAsync(int postId);

        Task<IEnumerable<PostDto>> GetAllPostsAsync();

        Task<bool> UpdatePostAsync(int postId, PostDto postDto);

        Task<bool> DeletePostAsync(int postId);

    }
}