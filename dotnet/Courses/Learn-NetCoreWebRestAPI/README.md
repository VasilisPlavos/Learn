# Things to remember
### Database: PM Console database commands
Source: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio

* install nuget package Microsoft.EntityFrameworkCore.Tools
* add-migration AddNationalParkToDb
* update-database
## When more that 1 database exist
* Add-Migration AddNationalParkToDb -Context AppDbContext
* update-database -Context AppDbContext

### Disable warnings about commenting
Go to **Properties > Build > Errors and warnings > Suppress warnings > 1591**

Or edit **ParkyAPI\ParkyAPI.csproj**

### Allow Any Cors [for Development]
Source: https://stackoverflow.com/questions/44379560/how-to-enable-cors-in-asp-net-core-webapi

On Startup.cs file use this code:

1. This goes at the top
```
public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
```

2. This goes at the services
```
        public void ConfigureServices(IServiceCollection services)
        {
            // NOTE: I allow anything for test only. I must change this
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddDbContext<ContactContext>(options =>
```

3. This goes at the end
```
            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
```
