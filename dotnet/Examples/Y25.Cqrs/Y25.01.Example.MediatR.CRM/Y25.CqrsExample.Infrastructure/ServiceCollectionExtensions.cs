using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Y25.CqrsExample.Infrastructure.Databases;

namespace Y25.CqrsExample.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServiceCollection(this IServiceCollection services, string connectionString)
    {
        return InitServices(services, connectionString);
    }
    public static IServiceCollection AddInfrastructureServiceCollection(this IServiceCollection services)
    {
        const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=contacts-dev;Trusted_Connection=True;MultipleActiveResultSets=true";
        return InitServices(services, connectionString);
    }

    private static IServiceCollection InitServices(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        return services;
    }
}
