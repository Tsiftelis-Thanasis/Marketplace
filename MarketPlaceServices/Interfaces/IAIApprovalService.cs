using MarketPlaceDTO;
using MarketPlaceModels.Models;

namespace MarketplaceServices.Interfaces
{
    public interface IAIApprovalService
    {
        Task<ApprovalResult> ApprovePostAsync(PostDto postDto);
    }
}