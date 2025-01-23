using MarketPlaceDTO;

namespace MarketplaceServices.Interfaces
{
    public interface IAIApprovalService
    {
        Task<ApprovalResult> ApprovePostAsync(PostDto postDto);
    }

    public class ApprovalResult
    {
        public bool IsApproved { get; set; }
        public string Reason { get; set; }
    }

}
