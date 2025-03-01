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
    public class PostService : Service<Post, PostDto>, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAIApprovalService _aiApprovalService;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        public PostService(IPostRepository postRepository, IMapper mapper, IAIApprovalService aiApprovalService, IDistributedCache  cache) 
            : base(postRepository, mapper, cache)
        {
            _postRepository = postRepository;
            _aiApprovalService = aiApprovalService;
            _mapper = mapper;
            _cache = cache;
        }
     
    }
}