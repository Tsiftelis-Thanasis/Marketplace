using Marketplace.Models;
using MarketPlaceDTO;
using MarketplaceRepository.Interfaces;
using MarketplaceServices.Interfaces;

namespace MarketplaceServices.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAIApprovalService _aiApprovalService;

        public PostService(IPostRepository postRepository, IAIApprovalService aiApprovalService)
        {
            _postRepository = postRepository;
            _aiApprovalService = aiApprovalService;
        }

        public async Task<PostDto> CreatePostAsync(PostDto postDto)
        {
            // Call AI service to approve or reject the post
            var approvalResult = await _aiApprovalService.ApprovePostAsync(postDto);

            if (!approvalResult.IsApproved)
            {
                throw new Exception("Post was rejected by AI.");
            }

            var post = new Post
            {
                Title = postDto.Title,
                Description = postDto.Description,
                Price = postDto.Price,
                CreatedDate = DateTime.UtcNow,
                UserId = postDto.UserId
            };

            await _postRepository.AddAsync(post);

            // Optionally populate the ID and return
            postDto.Id = post.PostId;
            return postDto;
        }

        public async Task<PostDto> GetPostByIdAsync(int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
            {
                throw new Exception("Post not found");
            }

            return new PostDto
            {
                Id = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Price = post.Price,
                CreatedDate = post.CreatedDate,
                UserId = post.UserId
            };
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return posts.Select(post => new PostDto
            {
                Id = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Price = post.Price,
                CreatedDate = post.CreatedDate,
                UserId = post.UserId
            });
        }

        public async Task<bool> UpdatePostAsync(int postId, PostDto postDto)
        {
            try
            {
                var post = await _postRepository.GetByIdAsync(postId);
                if (post == null)
                    throw new Exception("Post not found");

                post.Title = postDto.Title;
                post.Description = postDto.Description;
                post.Price = postDto.Price;

                await _postRepository.UpdateAsync(post);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            try
            {
                var post = await _postRepository.GetByIdAsync(postId);
                if (post == null)
                    throw new Exception("Post not found");

                await _postRepository.DeleteAsync(post);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}