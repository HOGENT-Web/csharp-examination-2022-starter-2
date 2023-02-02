using ParkingLot.Services.Parkings;
using ParkingLot.Shared.Parkings;
using Microsoft.Extensions.DependencyInjection;

namespace ParkingLot.Services;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all services to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddParkingServices(this IServiceCollection services)
    {
        services.AddScoped<IParkLocationService, ParkLocationService>();

        // Add more services here...

        return services;
    }
}

