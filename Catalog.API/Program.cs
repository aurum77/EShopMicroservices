using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.PipelineBehaviors;
using FastEndpoints;

var assembly = typeof(Program).Assembly;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddFastEndpoints();
builder
    .Services.AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("PostgreSQL")!);
    })
    .UseLightweightSessions();
builder.Services.AddLogging();
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
app.UseFastEndpoints();
app.UseExceptionHandler(options => { });
app.Run();
