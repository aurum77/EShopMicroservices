using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to DI container
builder.Services
    .AddInfrastructureServices();

var app = builder.Build();
// Configure the request pipeline

app.Run();
