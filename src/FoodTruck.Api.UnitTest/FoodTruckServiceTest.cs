using AutoMapper;
using FoodTruck.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using Xunit;

namespace FoodTruck.Api.UnitTest
{
    public class FoodTruckServiceTest : IClassFixture<FoodTruckDataFixture>
    {
        private readonly FoodTruckService _foodTruckService;

        private readonly Mock<ILogger<FoodTruckService>> mockLogger = new Mock<ILogger<FoodTruckService>>();


        private readonly Mock<IMapper> mockMapper = new Mock<IMapper>();

        public FoodTruckServiceTest(FoodTruckDataFixture foodTruckDataFixture)
        {
            _foodTruckService = new FoodTruckService(foodTruckDataFixture.FoodTruckContext, mockMapper.Object, mockLogger.Object);
        }

        [Fact]
        public async void Add_Existing_LocationID_Passed_ReturnsBadRequest()
        {
            // Arrange
            var mockFoodTruckDto = new Models.FoodTruckDto { LocationId = "xx" };
            var cts = new CancellationTokenSource();

            //Act
            var (errors, _) = await _foodTruckService.CreateFoodTruck(mockFoodTruckDto, cts.Token);

            //Assert
            Assert.Equal("Location Id already exists.", errors[0].ErrorMessage);
        }

        [Fact]
        public async void Add_Empty_LocationID_Passed_ReturnsBadRequest()
        {
            // Arrange
            var mockFoodTruckDto = new Models.FoodTruckDto { LocationId = "" };
            var cts = new CancellationTokenSource();

            //Act
            var (errors, _) = await _foodTruckService.CreateFoodTruck(mockFoodTruckDto, cts.Token);

            //Assert
            Assert.Equal("Location Id cannot be empty.", errors[0].ErrorMessage);
        }


        [Fact]
        public async void Add_Invalid_Latitude_Passed_ReturnsBadRequest()
        {
            // Arrange
            var mockFoodTruckDto = new Models.FoodTruckDto { LocationId = "xxx", Latitude = -110 };
            var cts = new CancellationTokenSource();

            //Act
            var (errors, _) = await _foodTruckService.CreateFoodTruck(mockFoodTruckDto, cts.Token);

            //Assert
            Assert.Equal("Latitude must be between -90 and 90 degrees.", errors[0].ErrorMessage);
        }

    }
}
