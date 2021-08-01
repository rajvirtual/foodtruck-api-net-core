using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Api.Models
{
    public class FoodTruckDto
    {
        [Required]
        public string LocationId { get; set; }

        [Required]
        public string Applicant { get; set; }

        public string FacilityType { get; set; }

        public string Cnn { get; set; }

        public string LocationDescription { get; set; }

        [Required]
        public string Address { get; set; }

        public string BlockLot { get; set; }

        [DefaultValue("")]
        public string Block { get; set; }

        public string Lot { get; set; }

        public string Permit { get; set; }

        public string FoodItems { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Schedule { get; set; }

        public string DaysHours { get; set; }

        public bool PriorPermit { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }
    }
}
