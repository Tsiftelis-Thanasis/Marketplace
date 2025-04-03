using MarketPlaceDTO;
using MarketPlaceModels.Models;
using MarketplaceServices.Interfaces;
using System.Text;
using System.Text.Json;

namespace MarketplaceServices.Services
{
    public class AIApprovalService : IAIApprovalService
    {
        private readonly HttpClient _httpClient;
        private readonly string _moderationEndpoint = "https://<your-region>.api.cognitive.microsoft.com/contentmoderator/moderate/v1.0/ProcessText/Screen";
        private readonly string _subscriptionKey = "<your-azure-key>";

        public AIApprovalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApprovalResult> ApprovePostAsync(PostDto postDto)
        {
            // Validate title is not empty
            if (string.IsNullOrWhiteSpace(postDto.Title))
            {
                return new ApprovalResult
                {
                    IsApproved = false,
                    Reason = "Title cannot be empty."
                };
            }

            // Check for inappropriate content using Azure Content Moderator
            var titleCheck = await AnalyzeTextAsync(postDto.Title);
            var descriptionCheck = await AnalyzeTextAsync(postDto.Description);

            if (!titleCheck.IsApproved || !descriptionCheck.IsApproved)
            {
                return new ApprovalResult
                {
                    IsApproved = false,
                    Reason = titleCheck.Reason ?? descriptionCheck.Reason
                };
            }

            // Approve post if content is safe
            return new ApprovalResult
            {
                IsApproved = true
            };
        }

        private async Task<ApprovalResult> AnalyzeTextAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new ApprovalResult { IsApproved = true };
            }

            var requestBody = JsonSerializer.Serialize(new { text });
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);

            var response = await _httpClient.PostAsync(_moderationEndpoint, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            // Parse response
            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ContentModerationResponse>(responseBody);

                if (result?.Classification?.ReviewRecommended == true)
                {
                    return new ApprovalResult
                    {
                        IsApproved = false,
                        Reason = "Content flagged for review due to potential violations."
                    };
                }
            }

            return new ApprovalResult { IsApproved = true };
        }
    }
}