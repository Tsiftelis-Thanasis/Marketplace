﻿using AutoMapper;
using Marketplace.Models;
using MarketPlaceDTO;
using MarketPlaceModels.Enums;
using MarketPlaceModels.Models;
using MarketplaceRepository.Interfaces;
using MarketplaceServices.Interfaces;
using MarketPlaceServices.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MarketplaceServices.Services
{
    public class UserDtoService : Service<User, UserDto>, IUserDtoService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public UserDtoService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper, IDistributedCache cache) :
            base(userRepository, mapper, cache)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<LoginResponse> LoginUserAsync(UserDto userDto)
        {
            var user = await _userRepository.GetUserByUsernameOrEmailAsync(userDto.Username, userDto.Email);

            if (user == null)
                return null; // User not found

            // Generate JWT token
            var token = GenerateUserJwtToken(_mapper.Map<UserDto>(user));

            return new LoginResponse { Token = token };
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