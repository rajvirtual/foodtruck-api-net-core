using AutoMapper;

namespace FoodTruck.Api.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Models.FoodTruckDto, Models.FoodTruck>();
        }
    }
}
