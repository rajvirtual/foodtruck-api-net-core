using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace FoodTruck.Api.Services
{
    public interface IFoodTruckService
    {
        public Task<Models.FoodTruck> GetByLocationId(string locationId, CancellationToken cancellationToken);

        public Task<IEnumerable<Models.FoodTruck>> GetByBlock(string block, CancellationToken cancellationToken);

        public Task<(List<ValidationResult>, Models.FoodTruck)> CreateFoodTruck(Models.FoodTruckDto foodTruckDto, CancellationToken cancellationToken);

        public Task<bool> FoodTruckExists(string locationId, CancellationToken cancellationToken);
    }
}
