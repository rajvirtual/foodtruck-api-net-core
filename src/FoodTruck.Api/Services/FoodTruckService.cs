using AutoMapper;
using FoodTruck.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodTruck.Api.Services
{
    public class FoodTruckService : IFoodTruckService
    {
        private readonly FoodTruckContext _foodTruckContext;
        private readonly IMapper _mapper;
        private readonly ILogger<FoodTruckService> _logger;

        public FoodTruckService(FoodTruckContext foodTruckContext, IMapper mapper, ILogger<FoodTruckService> logger)
        {
            _foodTruckContext = foodTruckContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Models.FoodTruck> GetByLocationId(string locationId, CancellationToken cancellationToken)
        {
            return await _foodTruckContext.FoodTrucks.FirstOrDefaultAsync(f => f.LocationId == locationId, cancellationToken);
        }

        public async Task<IEnumerable<Models.FoodTruck>> GetByBlock(string block, CancellationToken cancellationToken)
        {
            return await _foodTruckContext.FoodTrucks.Where(f => f.Block == block).ToListAsync(cancellationToken);
        }

        public async Task<bool> FoodTruckExists(string locationId, CancellationToken cancellationToken)
        {
            return await _foodTruckContext.FoodTrucks.AnyAsync(f => f.LocationId == locationId, cancellationToken);
        }

        public async Task<(List<ValidationResult>, Models.FoodTruck)> CreateFoodTruck(Models.FoodTruckDto foodTruckDto, CancellationToken cancellationToken)
        {
            var errors = ValidateInput(foodTruckDto);

            if (errors.Any())
            {
                return (errors, null);
            }

            var mappedFoodTruck = _mapper.Map<Models.FoodTruck>(foodTruckDto);

            mappedFoodTruck.Status = "REQUESTED";

            var dateTimeOffset = DateTimeOffset.Parse(DateTime.Now.Date.ToString(), null);

            mappedFoodTruck.Received = dateTimeOffset.Date;

            mappedFoodTruck.ExpirationDate = null;

            if (foodTruckDto.Block == null)
            {
                mappedFoodTruck.Block = "";
            }

            mappedFoodTruck.Location = $"({foodTruckDto.Latitude},{foodTruckDto.Longitude})";

            await _foodTruckContext.FoodTrucks.AddAsync(mappedFoodTruck, cancellationToken);

            await _foodTruckContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Food Truck was created {LocationId}", foodTruckDto.LocationId);

            return (errors, mappedFoodTruck);

        }

        private List<ValidationResult> ValidateInput(Models.FoodTruckDto foodTruckDto)
        {
            var list = new List<ValidationResult>();

            if (string.IsNullOrEmpty(foodTruckDto.LocationId))
            {
                list.Add(new ValidationResult("Location Id cannot be empty.", new[] { "Location Id" }));
            }

            if (_foodTruckContext.FoodTrucks.Any(f => f.LocationId == foodTruckDto.LocationId))
            {
                list.Add(new ValidationResult("Location Id already exists.", new[] { "Location Id" }));
            }

            if (foodTruckDto.Latitude != 0 && (foodTruckDto.Latitude < -90 ||
                  foodTruckDto.Latitude > 90))
            {
                list.Add(new ValidationResult("Latitude must be between -90 and 90 degrees.", new[] { "Latitude" }));
            }

            if (foodTruckDto.Longitude != 0 && (foodTruckDto.Longitude < -180 ||
                foodTruckDto.Longitude > 180))
            {

                list.Add(new ValidationResult("Longitude must be between -180 and 180 degrees.", new[] { "Longitude" }));
            }

            return list;
        }
    }
}
