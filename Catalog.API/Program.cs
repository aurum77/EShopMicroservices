var builder = WebApplication.CreateBuilder(args);

// Add services to DI container
var app = builder.Build();

// Configure HTTP request pipeline
app.MapGet("/", () => "Hello World!");

app.Run();