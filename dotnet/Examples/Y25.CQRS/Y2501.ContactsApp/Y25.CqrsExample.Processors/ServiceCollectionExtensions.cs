using Microsoft.Extensions.DependencyInjection;

namespace Y25.CqrsExample.Processors;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProcessorServiceCollection(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        return services;
    }
}