using FluentAssertions;
using Marketplace.Models;
using MarketplaceAPI.Controllers;
using MarketPlaceDTO;
using MarketPlaceModels.Models;
using MarketplaceServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MarketPlaceTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserDtoService> _mockService;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockService = new Mock<IUserDtoService>();
            var mockHashPassword = new Mock<IPasswordHasher<UserDto>>();
            _controller = new UserController(_mockService.Object, mockHashPassword.Object);
        }

        [Fact]
        public async Task Register_ReturnsCreatedAtAction_WhenUserIsValid()
        {
            var newUser = new UserDto { Username = "testuser", Email = "test@example.com" };
            var newRegisterUser = new RegisterModel { Username = "testuser", Email = "test@example.com", Password = "123456" };

            _mockService.Setup(s => s.RegisterUserAsync(It.IsAny<UserDto>())).ReturnsAsync(newUser);
            _mockService.Setup(s => s.UserExistsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _controller.Register(newRegisterUser);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedUser = Assert.IsType<User>(createdAtActionResult.Value);
            Assert.Equal(newUser.Username, returnedUser.Username);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequest = new LoginModel { Username = "user1", Password = "password123" };
            var loginResponse = new LoginResponse { Token = "mockToken" };

            _mockService.Setup(s => s.LoginUserAsync(It.IsAny<UserDto>())).ReturnsAsync(loginResponse);

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            result.Should().NotBeNull();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var token = okResult.Value as LoginResponse;
            token.Token.Should().Be("mockToken");
        }
    }
}