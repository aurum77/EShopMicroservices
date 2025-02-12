using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext : DbContext
{
    public DiscountContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Coupon>()
            .HasData(
                new Coupon
                {
                    Id = 1,
                    ProductName = "IPhone 14",
                    Description = "Student discount",
                    Amount = 150,
                },
                new Coupon
                {
                    Id = 2,
                    ProductName = "MacBook Air M3",
                    Description = "Student discount",
                    Amount = 100,
                }
            );
    }

    public DbSet<Coupon> Coupons { get; set; } = default!;
}
