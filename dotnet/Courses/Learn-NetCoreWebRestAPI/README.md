# Things to remember
### Database: PM Console database commands
* add-migration AddNationalParkToDb
* update-database

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
