using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Project.Web2.Controllers;
using Project.Web2.Models;
using Xunit;

namespace Project.Web2.Test {
    public class ShoppingCartControllerTest {
        public ShoppingCartControllerTest() {
            var fakeService = new Fake<IShoppingCartService>();
            var nameMissingItem = new ShoppingItem {
                Manufacturer = "Guinness",
                Price = 12.00M
            };
            fakeService.CallsTo(d => d.Add(nameMissingItem))
                .Returns(nameMissingItem);
            _service = fakeService.FakedObject;

            _controller = new ShoppingCartController(fakeService.FakedObject);
        }

        private readonly ShoppingCartController _controller;
        private readonly IShoppingCartService _service;

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest() {
            // Arrange
            var nameMissingItem = new ShoppingItem {
                Manufacturer = "Guinness",
                Price = 12.00M
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.Post(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        //[Fact]
        //public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem() {
        //    // Arrange
        //    var testItem = new ShoppingItem {
        //        Name = "Guinness Original 6 Pack",
        //        Manufacturer = "Guinness",
        //        Price = 12.00M
        //    };

        //    // Act
        //    var createdResponse =
        //        _controller.Post(testItem) as CreatedAtActionResult;
        //    var item = createdResponse.Value as ShoppingItem;

        //    // Assert
        //    Assert.IsType<ShoppingItem>(item);
        //    Assert.Equal("Guinness Original 6 Pack", item.Name);
        //}


        //[Fact]
        //public void Add_ValidObjectPassed_ReturnsCreatedResponse() {
        //    // Arrange
        //    var testItem = new ShoppingItem {
        //        Name = "Guinness Original 6 Pack",
        //        Manufacturer = "Guinness",
        //        Price = 12.00M
        //    };

        //    // Act
        //    var createdResponse = _controller.Post(testItem);

        //    // Assert
        //    Assert.IsType<CreatedAtActionResult>(createdResponse);
        //}

        //[Fact]
        //public void Get_WhenCalled_ReturnsAllItems() {
        //    // Act
        //    var okResult = _controller.Get().Result as OkObjectResult;

        //    // Assert
        //    var items = Assert.IsType<List<ShoppingItem>>(okResult.Value);
        //    Assert.Equal(3, items.Count);
        //}

        //[Fact]
        //public void Get_WhenCalled_ReturnsOkResult() {
        //    // Act
        //    var okResult = _controller.Get();

        //    // Assert
        //    Assert.IsType<OkObjectResult>(okResult.Result);
        //}

        //[Fact]
        //public void GetById_ExistingGuidPassed_ReturnsOkResult() {
        //    // Arrange
        //    var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

        //    // Act
        //    var okResult = _controller.Get(testGuid);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(okResult.Result);
        //}

        //[Fact]
        //public void GetById_ExistingGuidPassed_ReturnsRightItem() {
        //    // Arrange
        //    var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

        //    // Act
        //    var okResult = _controller.Get(testGuid).Result as OkObjectResult;

        //    // Assert
        //    Assert.IsType<ShoppingItem>(okResult.Value);
        //    Assert.Equal(testGuid, (okResult.Value as ShoppingItem).Id);
        //}

        //[Fact]
        //public void GetById_UnknownGuidPassed_ReturnsNotFoundResult() {
        //    // Act
        //    var notFoundResult = _controller.Get(Guid.NewGuid());

        //    // Assert
        //    Assert.IsType<NotFoundResult>(notFoundResult.Result);
        //}

        //[Fact]
        //public void Remove_ExistingGuidPassed_RemovesOneItem() {
        //    // Arrange
        //    var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

        //    // Act
        //    var okResponse = _controller.Remove(existingGuid);

        //    // Assert
        //    Assert.Equal(2, _service.GetAllItems().Count());
        //}

        //[Fact]
        //public void Remove_ExistingGuidPassed_ReturnsOkResult() {
        //    // Arrange
        //    var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

        //    // Act
        //    var okResponse = _controller.Remove(existingGuid);

        //    // Assert
        //    Assert.IsType<OkResult>(okResponse);
        //}

        //[Fact]
        //public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse() {
        //    // Arrange
        //    var notExistingGuid = Guid.NewGuid();

        //    // Act
        //    var badResponse = _controller.Remove(notExistingGuid);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(badResponse);
        //}
    }
}