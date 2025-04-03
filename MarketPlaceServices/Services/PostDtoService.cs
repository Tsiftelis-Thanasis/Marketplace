using AutoMapper;
using Marketplace.Models;
using MarketPlaceDTO;
using MarketplaceRepository.Interfaces;
using MarketplaceRepository.Repositories;
using MarketplaceServices.Interfaces;
using MarketPlaceServices.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace MarketplaceServices.Services
{
    public class PostDtoService : Service<Post, PostDto>, IPostDtoService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAIApprovalService _aiApprovalService;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        
        public PostDtoService(IPostRepository postRepository, IMapper mapper, IAIApprovalService aiApprovalService, IDistributedCache  cache) 
            : base(postRepository, mapper, cache)
        {
            _postRepository = postRepository;
            _aiApprovalService = aiApprovalService;
            _mapper = mapper;
            _cache = cache;
        }


        public override async Task<PostDto> CreateAsync(PostDto postDto)
        {
            var approvalResult = await _aiApprovalService.ApprovePostAsync(postDto);

            if (!approvalResult.IsApproved)
            {
                throw new InvalidOperationException($"Post rejected: {approvalResult.Reason}");
            }

            return await base.CreateAsync(postDto);
        }

    }
}