using Microsoft.AspNetCore.Mvc;
using Moq;
using MarketplaceAPI.Controllers;
using Marketplace.Models;
using MarketplaceAPI.Services.Interfaces;
using Xunit;
using Microsoft.AspNetCore.Identity;

public class UserControllerTests
{
    [Fact]
    public async Task Register_ReturnsCreatedAtAction_WhenUserIsValid()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var mockHashPassword = new Mock<IPasswordHasher<User>>();
        var newUser = new User { Username = "testuser", Email = "test@example.com" };
        var newRegisterUser = new RegisterModel { Username = "testuser", Email = "test@example.com", Password = "123456" };

        mockUserService.Setup(s => s.RegisterUserAsync(It.IsAny<User>())).ReturnsAsync(newUser);
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
