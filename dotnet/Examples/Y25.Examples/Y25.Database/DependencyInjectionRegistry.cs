using Microsoft.EntityFrameworkCore;
using Y25.Database.Helpers;

namespace Y25.Database;

public static class DependencyInjectionRegistry
{
    public static async void AddDbServices(this IServiceCollection services, IConfiguration builderConfiguration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlServer(builderConfiguration.GetConnectionString("DbConnection-Testing")));

        services.AddScoped<DbHelper>();

        using (var serviceProvider = services.BuildServiceProvider())
        {
            var dbHelper = serviceProvider.GetRequiredService<DbHelper>();
            await dbHelper.GenerateFakeContactsAsync();
        }

    }
}