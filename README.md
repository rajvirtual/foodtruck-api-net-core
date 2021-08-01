# San Francisco Food Truck Api

A .Net Core Web Api to locate San Francisco Food Trucks.

[Demo Site](https://foodtruckapideploy.azurewebsites.net/swagger/index.html)

### How it works

This Web Api allows consumers to

* Add a new food truck.
* Retrieve a food truck based on the locationid field.
* Get all food trucks for a given block.

This project is using .Net 6 preview version along with :

[AutoMapper](https://automapper.org/) to map the Food Truck Dto to Entity 

[Entity Framework Core InMemoryDatabase](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli) for demo purposes to store and easy retrieval of food trucks.  

[CsvHelper](https://joshclose.github.io/CsvHelper/) for reading sample food truck CSV file.

[ASP.NET Core MVC Versioning](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Versioning/) to version the endpoints.

[Swagger UI](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) to test the endpoints.


### Setup

Install Visual Studio 2022 Preview version from [here](https://visualstudio.microsoft.com/vs/preview/vs2022/)

Install .Net 6 preview version from [here](https://dotnet.microsoft.com/download/dotnet/6.0)


### Build

To build and test the web api

* Go to the project folder (FoodTruck.Api) and build

   `dotnet build`

* Run the project

   `dotnet run`

### Swagger Url

Navigate to http://localhost:5000/swagger to test the endpoints.

### Unit Tests

To execute unit tests

Go to the test folder FoodTruck.Api.UnitTest and run

   `dotnet test`

