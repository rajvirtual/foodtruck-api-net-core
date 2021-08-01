using FoodTruck.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace FoodTruck.Api.UnitTest
{
    public class FoodTruckDataFixture :IDisposable
    {
        public FoodTruckContext FoodTruckContext { get; private set; }

        public FoodTruckDataFixture()
        {
            var options = new DbContextOptionsBuilder<FoodTruckContext>()
                .UseInMemoryDatabase(databaseName: "FoodTrucks")
                .Options;

            FoodTruckContext = new FoodTruckContext(options);
            FoodTruckContext.FoodTrucks.Add(new Models.FoodTruck { LocationId = "xx" });
            FoodTruckContext.SaveChanges();
        }

        public void Dispose()
        {
            FoodTruckContext.Dispose();
        }
    }
}
