using FoodTruck.Api.Infrastructure;
using FoodTruck.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddControllersWithSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Food Truck Api", Description = "Api To Find Food Trucks In San Francisco City", Version = "v1" });
        });

        builder.Services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        return builder;
    }

    public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(
                typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly);

        builder.Services.AddScoped<IFoodTruckService, FoodTruckService>();

        builder.Services.AddDbContext<FoodTruckContext>(options => options.UseInMemoryDatabase(databaseName: "FoodTrucks"));

        return builder;
    }
}

