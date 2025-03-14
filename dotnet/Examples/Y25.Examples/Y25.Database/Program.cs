using Y25.Database;

var builder = WebApplication.CreateBuilder(args);

// add-migration db1
// update-database
builder.Services.AddDbServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Database project is running...");
app.Run();


