using Y25.Database;

var builder = WebApplication.CreateBuilder(args);


// Use the Package Manager Console to make database connection and push entities to database.
// On Package Manager Console select `Y25.Database` as Default project
// Right click on `Y25.Database` project and Set as startup project`
// add-migration db1
// update-database
builder.Services.AddDbServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Database project is running...");
app.Run();


