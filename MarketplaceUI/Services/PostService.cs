using Marketplace.Models;
using MarketplaceUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

public class PostService: IPostService
{

    private readonly HttpClient _httpClient;

    public PostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
