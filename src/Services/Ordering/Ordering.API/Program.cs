using Ordering.API;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to DI container
builder.Services.AddInfrastructureServices(configuration);

var app = builder.Build();

// Configure the request pipeline

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.Run();
