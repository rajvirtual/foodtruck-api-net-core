using CsvHelper.Configuration;
using FoodTruck.Api.Models;

namespace FoodTruck.Api.Mapping
{
    public class CsvMapping : ClassMap<Models.FoodTruck>
    {
        public CsvMapping()
        {
            Map(f => f.LocationId).Name("locationid");
            Map(f => f.Applicant).Name("Applicant");
            Map(f => f.FacilityType).Name("FacilityType");
            Map(f => f.Cnn).Name("cnn");
            Map(f => f.LocationDescription).Name("LocationDescription");
            Map(f => f.Address).Name("Address");
            Map(f => f.BlockLot).Name("blocklot");
            Map(f => f.Block).Name("block");
            Map(f => f.Lot).Name("lot");
            Map(f => f.Permit).Name("permit");
            Map(f => f.Status).Name("Status");
            Map(f => f.FoodItems).Name("FoodItems");
            Map(f => f.X).Name("X");
            Map(f => f.Y).Name("Y");
            Map(f => f.Latitude).Name("Latitude");
            Map(f => f.Longitude).Name("Longitude");
            Map(f => f.Schedule).Name("Schedule");
            Map(f => f.DaysHours).Name("dayshours");
            Map(f => f.NOISent).Name("NOISent");
            Map(f => f.Approved).Name("Approved");
            Map(f => f.Received).Name("Received");
            Map(f => f.PriorPermit).Name("PriorPermit");
            Map(f => f.ExpirationDate).Name("ExpirationDate");
            Map(f => f.Location).Name("Location");
        }
    }
}
