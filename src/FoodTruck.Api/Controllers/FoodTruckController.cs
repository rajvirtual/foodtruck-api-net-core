using FoodTruck.Api.Models;
using FoodTruck.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodTruck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    //[Authorize]
    public class FoodTruckController : ControllerBase
    {
        private readonly ILogger<FoodTruckController> _logger;
        private readonly IFoodTruckService _foodTruckService;

        public FoodTruckController(ILogger<FoodTruckController> logger, IFoodTruckService foodTruckService)
        {
            _logger = logger;
            _foodTruckService = foodTruckService;
        }

        [HttpGet]
        [Route("GetByLocationId/{locationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetByLocationId(string locationId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(locationId))
            {
                ModelState.AddModelError("LocationId", "Location Id cannot be empty");
                return BadRequest(ModelState);
            }

            var foodTruck = await _foodTruckService.GetByLocationId(locationId, cancellationToken);
            if (foodTruck != null)
            {
                return Ok(foodTruck);
            }

            return NotFound($"Food Truck not found for Location Id {locationId}");
        }

        [HttpGet]
        [Route("GetByBlock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetByBlock([FromQuery] string block, CancellationToken cancellationToken)
        {
            var foodTrucks = await _foodTruckService.GetByBlock(block ?? "", cancellationToken);
            if (foodTrucks != null)
            {
                return Ok(foodTrucks);
            }

            return NotFound();
        }

        [HttpPost]
        [Route(nameof(CreateFoodTruck))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CreateFoodTruck(FoodTruckDto foodTruckDto, CancellationToken cancellationToken)
        {
            try
            {
                if (foodTruckDto == null)
                {
                    return BadRequest("Food Truck cannot be null.");
                }

                var errors = await _foodTruckService.CreateFoodTruck(foodTruckDto, cancellationToken);

                if (errors.Any())
                {
                    return BadRequest(errors);
                }

                return Created($"/{nameof(CreateFoodTruck)}/{foodTruckDto.LocationId}", foodTruckDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception happened in CreateFoodTruck.");
                throw;
            }
        }
    }
}
