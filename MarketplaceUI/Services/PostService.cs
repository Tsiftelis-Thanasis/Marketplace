using Marketplace.Models;
using MarketPlaceDTO;
using MarketplaceServices.Interfaces;
using MarketplaceUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class PostService : IPostService
{
    private readonly IAIApprovalService _aiApprovalService;

    private readonly HttpClient _httpClient;

    public PostService(HttpClient httpClient, IAIApprovalService aiApprovalService)
    {
        _httpClient = httpClient;
        _aiApprovalService = aiApprovalService;
    }

    public async Task<List<Post>> GetPostsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Post>>("api/post");
    }

    public async Task<Post?> GetPostByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Post>($"api/post/{id}");
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
       

        var response = await _httpClient.PostAsJsonAsync("api/post", post);
        return await response.Content.ReadFromJsonAsync<Post>();
    }

    public async Task<Post> UpdatePostAsync(Post post)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/post/{post.PostId}", post);
        return await response.Content.ReadFromJsonAsync<Post>();
    }

    public async Task<bool> DeletePostAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/post/{id}");
        return response.IsSuccessStatusCode;
    }
}