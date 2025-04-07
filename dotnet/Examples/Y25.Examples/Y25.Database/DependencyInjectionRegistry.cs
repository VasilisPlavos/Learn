using Microsoft.EntityFrameworkCore;
using Y25.Database.Helpers;

namespace Y25.Database;

public static class DependencyInjectionRegistry
{
    public static async void AddDbServices(this IServiceCollection services, IConfiguration builderConfiguration)
    {
        var connectionString = builderConfiguration.GetConnectionString("DbConnection-Testing");
        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

        services.AddScoped<DbHelper>();

        using (var serviceProvider = services.BuildServiceProvider())
        {
            var dbHelper = serviceProvider.GetRequiredService<DbHelper>();
            await dbHelper.GenerateFakeContactsAsync();
        }

    }
}