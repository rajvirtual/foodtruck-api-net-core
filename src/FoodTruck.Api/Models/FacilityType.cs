using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Api.Models
{
    public class FacilityType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
