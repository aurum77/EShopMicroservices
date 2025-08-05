using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to DI container
builder.Services
    .AddInfrastructureServices(configuration);

var app = builder.Build();
// Configure the request pipeline

app.Run();
