using Microsoft.EntityFrameworkCore.Design;

namespace Ciroman.Web.Contexts;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseSqlServer("Server=localhost;Database=OrderDb;User Id=sa;Password=123lkjsalkjal.dla;Encrypt=False; TrustServerCertificate=True");
        return new(dbContextOptionsBuilder.Options);
    }
}
