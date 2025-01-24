using AutoMapper;
using Marketplace.Models;
using MarketPlaceDTO;
using MarketplaceRepository.Interfaces;
using MarketplaceRepository.Repositories;
using MarketplaceServices.Interfaces;
using MarketPlaceServices.Services;

namespace MarketplaceServices.Services
{
    public class PostService : Service<Post, PostDto>, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAIApprovalService _aiApprovalService;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper, IAIApprovalService aiApprovalService) : base(postRepository, mapper)
        {
            _postRepository = postRepository;
            _aiApprovalService = aiApprovalService;
            _mapper = mapper;
        }
     
    }
}