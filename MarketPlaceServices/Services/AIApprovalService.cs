using MarketPlaceDTO;
using MarketplaceServices.Interfaces;

namespace MarketplaceServices.Services
{
    public class AIApprovalService : IAIApprovalService
    {
        public async Task<ApprovalResult> ApprovePostAsync(PostDto postDto)
        {
            // Simulate AI approval logic
            // For example, reject posts with empty titles
            if (string.IsNullOrWhiteSpace(postDto.Title))
            {
                return new ApprovalResult
                {
                    IsApproved = false,
                    Reason = "Title cannot be empty."
                };
            }

            // Approve other posts
            return new ApprovalResult
            {
                IsApproved = true
            };
        }
    }

}
