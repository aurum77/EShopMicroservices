using Discount.Grpc.Data;
using Discount.Grpc.Servicescount;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();

app.Run();
