using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Y25.ManyProcessors.Processors;

namespace Y25.ManyProcessors;

public static class DependencyInjectionRegistry
{
    public static async void AddManyProcessorsServices(this IServiceCollection services,
        IConfiguration builderConfiguration)
    {
        services.AddScoped<ITransactionService, TransactionService>();
    }
}