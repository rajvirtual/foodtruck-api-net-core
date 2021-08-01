using CsvHelper;
using FoodTruck.Api.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FoodTruck.Api.Infrastructure
{
    public class SeedDataLoader
    {
        public static async Task LoadAsync(IServiceProvider serviceProvider)
        {
            using (var context = new FoodTruckContext(serviceProvider.GetRequiredService<DbContextOptions<FoodTruckContext>>()))
            {
                if (await context.FoodTrucks.AnyAsync())
                {
                    return;
                }
                var assembly = typeof(SeedDataLoader).GetTypeInfo().Assembly;
                
                var path = $"{assembly.GetName().Name}.{"Setup.FoodTrucksData.csv"}";

                using var resource = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Setup.FoodTrucksData.csv");
              
                using var reader = new StreamReader(resource);
                
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                
                csv.Context.RegisterClassMap<CsvMapping>();
                
                var records = csv.GetRecords<Models.FoodTruck>();

                await context.FoodTrucks.AddRangeAsync(records);

                await context.Statuses.AddRangeAsync(GetStatuses());

                await context.FacilityTypes.AddRangeAsync(GetFacilityTypes());

                await context.SaveChangesAsync();

                var len = records.Count();
            }
        }

        private static IEnumerable<Models.Status> GetStatuses()
        {
            return new List<Models.Status>
        {
            new Models.Status{ Id = 1, Name ="REQUESTED"},
            new Models.Status{ Id = 2, Name ="EXPIRED"},
            new Models.Status{ Id = 3, Name ="SUSPEND"},
            new Models.Status{ Id = 4, Name ="INACTIVE"},
            new Models.Status{ Id = 5, Name ="APPROVED"},
            new Models.Status{ Id = 6, Name ="ISSUED"},
            new Models.Status{ Id = 7, Name ="ONHOLD"}
        };
        }

        private static IEnumerable<Models.FacilityType> GetFacilityTypes()
        {
            return new List<Models.FacilityType>
        {
            new Models.FacilityType{ Id = 1, Name ="Truck"},
            new Models.FacilityType{ Id = 2, Name ="Push Cart"},
        };
        }
    }
}
