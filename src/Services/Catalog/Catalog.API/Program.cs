using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.PipelineBehaviors;
using FastEndpoints;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var assembly = typeof(Program).Assembly;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder
    .Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("PostgreSQL")!);
builder.Services.AddFastEndpoints();
builder
    .Services.AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("PostgreSQL")!);
    })
    .UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddLogging();
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
app.UseHealthChecks(
    "/health",
    new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse }
);
app.UseFastEndpoints();
app.UseExceptionHandler(options => { });
app.Run();
