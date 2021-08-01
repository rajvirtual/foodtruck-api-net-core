using Microsoft.EntityFrameworkCore;

namespace FoodTruck.Api.Infrastructure
{
    public class FoodTruckContext : DbContext
    {
        public FoodTruckContext(DbContextOptions<FoodTruckContext> options) : base(options) { }

        public DbSet<Models.FoodTruck> FoodTrucks { get; set; }

        public DbSet<Models.Status> Statuses { get; set; }

        public DbSet<Models.FacilityType> FacilityTypes { get; set; }
    }
}
