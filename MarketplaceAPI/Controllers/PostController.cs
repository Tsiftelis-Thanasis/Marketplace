using MarketPlaceDTO;
using MarketplaceServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postService.GetAllPostsAsync();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(PostDto postDto)
    {
        var createdPost = await _postService.CreatePostAsync(postDto);
        return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, PostDto postDto)
    {
        var updatedPost = await _postService.UpdatePostAsync(id, postDto);
        if (!updatedPost)
        {
            return NotFound();
        }
        return Ok(updatedPost);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var result = await _postService.DeletePostAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}