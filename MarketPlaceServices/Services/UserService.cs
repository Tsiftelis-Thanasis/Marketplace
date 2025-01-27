using MarketplaceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MarketplaceAPI;
using Marketplace.Models;
using MarketplaceRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using MarketPlaceServices.Services;
using MarketPlaceDTO;
using AutoMapper;
using MarketplaceRepository.Repositories;
using MarketPlaceModels.Enums;
using MarketPlaceModels.Models;

namespace MarketplaceServices.Services
{
    public class UserService: Service<User, UserDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
             

        public async Task<UserDto?> GetUserByUsernameOrEmailAsync(string username, string email)
        {
            var user = await _userRepository.GetUserByUsernameOrEmailAsync(username, email);
            return _mapper.Map<UserDto>(user); 
        }

        public async Task<UserDto> RegisterUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.AddAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _userRepository.UserExistsAsync(username, email);
        }

        public LoginResponse LoginUser(UserDto userDto) 
        {
            var token = GenerateUserJwtToken(userDto);
            LoginResponse loginResponse = new LoginResponse { Token = token };
            return loginResponse;
        }

        public string GenerateUserJwtToken(UserDto user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, ((Roles)user.Role).ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
