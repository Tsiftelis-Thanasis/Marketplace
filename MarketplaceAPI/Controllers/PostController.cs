using Microsoft.AspNetCore.Mvc;

namespace MarketplaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="title">The title of the post.</param>
        /// <param name="description">The description of the post.</param>
        [HttpPost("create")]
        public IActionResult CreatePost(string title, string description)
        {
            // Placeholder logic
            return Ok($"Post '{title}' created successfully!");
        }

        /// <summary>
        /// Gets all posts.
        /// </summary>
        [HttpGet]
        public IActionResult GetPosts()
        {
            // Placeholder logic
            return Ok("Returning all posts.");
        }
    }
}

