// Initialise a web application from our console application
var app = WebApplication.CreateBuilder(args).Build();

// Map the default route
app.MapGet("/", () => "Hello world");

// run the application
app.Run();