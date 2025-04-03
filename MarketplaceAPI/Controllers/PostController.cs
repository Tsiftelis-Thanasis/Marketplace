using MarketPlaceDTO;
using MarketplaceServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostDtoService _postDtoService;

    public PostController(IPostDtoService postDtoService)
    {
        _postDtoService = postDtoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postDtoService.GetAllAsync();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = await _postDtoService.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(PostDto postDto)
    {
        if (postDto == null)
            return BadRequest("Invalid post data");

        var createdPost = await _postDtoService.CreateAsync(postDto);

        if (createdPost == null)
            return StatusCode(500, "Error creating post");

        return Ok(createdPost);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, PostDto postDto)
    {
        var updatedPost = await _postDtoService.UpdateAsync(id, postDto);
        if (!updatedPost)
        {
            return NotFound();
        }
        return Ok(updatedPost);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var result = await _postDtoService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var filePath = Path.Combine("wwwroot/uploads", file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok($"/uploads/{file.FileName}");
    }
}