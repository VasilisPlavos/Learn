using Example.Cloudon.API.Repository;
using Example.Cloudon.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Cloudon.API
{
    public static class DependencyInjectionRegistry
    {
        public static void AddMyServices(this IServiceCollection services)
        {
            // Repos here
            services.AddScoped<IProductRepository, ProductRepository>();

            // Services here
            services.AddScoped<IProductService, ProductService>();
        }
    }
}