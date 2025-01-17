using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
        {
            return;
        }

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    public static IEnumerable<Product> GetPreconfiguredProducts() =>
        new List<Product>()
        {
            new Product()
            {
                Id = new Guid("e39d8784-3083-4890-b573-1c241797309d"),
                Name = "Laptop",
                Category = new List<string>() { "Electronics", "Computers" },
                Description =
                    "High performance laptop with the latest processor and dedicated graphics card.",
                ImageFile = "laptop.jpg",
                Price = 1200.00m,
            },
            new Product()
            {
                Id = new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"),
                Name = "Coffee Maker",
                Category = new List<string>() { "Kitchen Appliances", "Coffee" },
                Description =
                    "Automatic coffee maker with built-in grinder for fresh coffee every morning.",
                ImageFile = "coffeemaker.jpg",
                Price = 75.00m,
            },
            new Product()
            {
                Id = new Guid("f0e9d8c7-b6a5-4321-0987-6543210fedcb"),
                Name = "T-Shirt",
                Category = new List<string>() { "Clothing", "Men's Fashion" },
                Description = "Classic cotton t-shirt in various colors.",
                ImageFile = "tshirt.jpg",
                Price = 20.00m,
            },
            new Product()
            {
                Id = new Guid("98765432-10fe-dcba-9876-543210fedcba"),
                Name = "Backpack",
                Category = new List<string>() { "Bags", "Travel" },
                Description = "Durable backpack with multiple compartments for everyday use.",
                ImageFile = "backpack.jpg",
                Price = 50.00m,
            },
            new Product()
            {
                Id = new Guid("abcdef01-2345-6789-abcd-ef0123456789"),
                Name = "Smartphone",
                Category = new List<string>() { "Electronics", "Mobile Phones" },
                Description =
                    "Flagship smartphone with advanced camera system and long battery life.",
                ImageFile = "smartphone.jpg",
                Price = 800.00m,
            },
        };
}
