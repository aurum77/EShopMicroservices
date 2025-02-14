using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.PipelineBehaviors;
using Discount.Grpc;
using FastEndpoints;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var assembly = typeof(Program).Assembly;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder
    .Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("PostgreSQL")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
builder.Services.AddFastEndpoints();
builder
    .Services.AddMarten(options =>
    {
        options.Connection(builder.Configuration.GetConnectionString("PostgreSQL")!);
        options.Schema.For<ShoppingCart>().Identity(x => x.Username);
    })
    .UseLightweightSessions();
builder.Services.AddLogging();
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    //options.InstanceName = "DistributedCache";
});
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});

var app = builder.Build();
app.UseHealthChecks(
    "/health",
    new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse }
);
app.UseFastEndpoints();
app.UseExceptionHandler(options => { });

app.Run();
