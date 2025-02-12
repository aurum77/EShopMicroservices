using Discount.Grpc.Data;
using Discount.Grpc.Servicescount;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
});

var app = builder.Build();
app.UseMigration();
app.MapGrpcService<DiscountService>();

app.Run();
