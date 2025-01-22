using MarketplaceAPI.Services;
using MarketplaceAPI.Services.Interfaces;
using MarketplaceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MarketplaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserController(IUserService userService, IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (await _userService.UserExistsAsync(model.Username, model.Email))
            {
                return BadRequest("User already exists.");
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Role = "Buyer",  // Set default role as "Buyer"
                CreatedDate = DateTime.UtcNow
            };

            // Hash the password and store it securely
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

            await _userService.RegisterUserAsync(user);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.GetUserByUsernameOrEmailAsync(model.Username, string.Empty);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Generate JWT token
            var token = _userService.GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
