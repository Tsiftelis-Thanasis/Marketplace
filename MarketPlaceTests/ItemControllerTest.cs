using FluentAssertions;
using MarketPlaceDTO;
using MarketplaceServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MarketPlaceTests
{
    public class ItemControllerTests
    {
        private readonly Mock<IItemDtoService> _mockService;
        private readonly ItemController _controller;

        public ItemControllerTests()
        {
            _mockService = new Mock<IItemDtoService>();
            _controller = new ItemController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllItems_ShouldReturnOkResult()
        {
            // Arrange
            var mockItems = new List<ItemDto>
            {
                 new ItemDto {  Id = 1, Description = "Test", Title = "Laptop", Price = 1200 },
                 new ItemDto {  Id = 2 , Description = "Asus", Title = "Laptop", Price = 1100 },
            };

            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(mockItems);

            // Act
            var result = await _controller.GetAllItems();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(mockItems); // Compare collections
        }
    }
}