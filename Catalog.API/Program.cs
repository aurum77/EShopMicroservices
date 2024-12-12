using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddFastEndpoints();
builder
    .Services.AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("PostgreSQL")!);
    })
    .UseLightweightSessions();
builder.Services.AddLogging();

var app = builder.Build();
app.UseFastEndpoints();
app.Run();
