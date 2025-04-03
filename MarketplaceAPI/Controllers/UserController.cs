using Marketplace.Models;
using MarketPlaceDTO;
using MarketPlaceModels.Enums;
using MarketplaceServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserDtoService _userService;
        private readonly IPasswordHasher<UserDto> _passwordHasher;

        public UserController(IUserDtoService userService, IPasswordHasher<UserDto> passwordHasher)
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

            var userDto = new UserDto
            {
                Username = model.Username,
                Email = model.Email,
                Role = (int)Roles.User,
                CreatedDate = DateTime.UtcNow
            };

            // Hash the password and store it securely
            userDto.PasswordHash = _passwordHasher.HashPassword(userDto, model.Password);

            await _userService.RegisterUserAsync(userDto);

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
            var token = _userService.GenerateUserJwtToken(user);

            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var Users = await _userService.GetAllAsync();
            return Ok(Users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto UserDto)
        {
            var createdUser = await _userService.CreateAsync(UserDto);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto UserDto)
        {
            var updatedUser = await _userService.UpdateAsync(id, UserDto);
            if (!updatedUser)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}