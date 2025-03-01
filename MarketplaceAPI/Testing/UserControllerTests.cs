using Microsoft.AspNetCore.Mvc;
using Moq;
using MarketplaceAPI.Controllers;
using Marketplace.Models;
using Xunit;
using Microsoft.AspNetCore.Identity;
using MarketPlaceDTO;
using MarketplaceServices.Interfaces;

public class UserControllerTests
{
    [Fact]
    public async Task Register_ReturnsCreatedAtAction_WhenUserIsValid()
    {
        // Arrange
        var mockUserService = new Mock<IUserDtoService>();
        var mockHashPassword = new Mock<IPasswordHasher<UserDto>>();
        var newUser = new UserDto { Username = "testuser", Email = "test@example.com" };
        var newRegisterUser = new RegisterModel { Username = "testuser", Email = "test@example.com", Password = "123456" };

        mockUserService.Setup(s => s.RegisterUserAsync(It.IsAny<UserDto>())).ReturnsAsync(newUser);
        mockUserService.Setup(s => s.UserExistsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

        var controller = new UserController(mockUserService.Object, mockHashPassword.Object);

        // Act
        var result = await controller.Register(newRegisterUser);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedUser = Assert.IsType<User>(createdAtActionResult.Value);
        Assert.Equal(newUser.Username, returnedUser.Username);
    }
}
