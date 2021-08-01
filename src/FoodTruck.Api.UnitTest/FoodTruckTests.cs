using FoodTruck.Api.Controllers;
using FoodTruck.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FoodTruck.Api.UnitTest
{
    public class FoodTruckTests
    {
        //Arrange
        private readonly Mock<IFoodTruckService> mock = new Mock<IFoodTruckService>();
        private readonly Mock<ILogger<FoodTruckController>> mockLogger = new Mock<ILogger<FoodTruckController>>();

        private readonly FoodTruckController _foodTruckController;

        public FoodTruckTests()
        {
            _foodTruckController = new FoodTruckController(mockLogger.Object, mock.Object);
        }

        [Fact]
        public async void GetByLocationId()
        {
            //Arrange
            var foodTruckExpected = new Models.FoodTruck { LocationId = "xx" };
            var cts = new CancellationTokenSource();

            mock.Setup(p => p.GetByLocationId("xx", cts.Token)).ReturnsAsync(foodTruckExpected);
            //Act
            var result = await _foodTruckController.GetByLocationId("xx", cts.Token) as ObjectResult;
            //Assert
            Assert.IsAssignableFrom<ActionResult>(result);
            Assert.True(foodTruckExpected.Equals(result.Value));
        }

        [Fact]
        public async void GetByBlock()
        {
            //Arrange
            var foodTrucksExpected = new List<Models.FoodTruck>() {
             new Models.FoodTruck { LocationId = "xx" },
             new Models.FoodTruck { LocationId = "yy" }};

            var cts = new CancellationTokenSource();

            mock.Setup(p => p.GetByBlock("xx", cts.Token)).ReturnsAsync(foodTrucksExpected);
            var ft = new FoodTruckController(mockLogger.Object, mock.Object);
            var result = await _foodTruckController.GetByBlock("xx", cts.Token) as ObjectResult;
            var foodTrucks = Assert.IsType<List<Models.FoodTruck>>(result.Value);
            Assert.Equal(2, foodTrucks.Count);
        }

        [Fact]
        public async void CreateFoodTruck()
        {
            var mockFoodTruckDto = new Models.FoodTruckDto { LocationId = "xx" };
            var cts = new CancellationTokenSource();
            mock.Setup(c => c.CreateFoodTruck(mockFoodTruckDto, cts.Token)).Returns(Task.FromResult(new List<ValidationResult>()));
            var badResponse = await _foodTruckController.CreateFoodTruck(mockFoodTruckDto, cts.Token);
            Assert.IsType<CreatedResult>(badResponse);
        }
    }
}
