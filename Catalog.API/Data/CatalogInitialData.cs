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
            new Product()
            {
                Id = new Guid("a1d5c3e2-5b6f-4d9c-a765-2e3b7f9d9f1a"),
                Name = "Smartphone",
                Category = new List<string>() { "Electronics", "Mobile" },
                Description =
                    "Latest smartphone with a high-resolution display and advanced camera system.",
                ImageFile = "smartphone.jpg",
                Price = 999.99m,
            },
            new Product()
            {
                Id = new Guid("f5a6c8d9-7e8b-4d3a-9f2c-6a5e7b8d9f0e"),
                Name = "Gaming Laptop",
                Category = new List<string>() { "Electronics", "Computers", "Gaming" },
                Description =
                    "Powerful gaming laptop with RGB lighting and high refresh rate display.",
                ImageFile = "gaming_laptop.jpg",
                Price = 1800.00m,
            },
            new Product()
            {
                Id = new Guid("b2c3d4e5-6f7a-4b8c-9d0e-1a2b3c4d5e6f"),
                Name = "Wireless Headphones",
                Category = new List<string>() { "Electronics", "Audio" },
                Description = "Noise-canceling wireless headphones with long battery life.",
                ImageFile = "headphones.jpg",
                Price = 249.99m,
            },
            new Product()
            {
                Id = new Guid("d9e8f7a6-5b4c-3d2e-1f0a-9b8c7d6e5f4a"),
                Name = "Mechanical Keyboard",
                Category = new List<string>() { "Electronics", "Accessories" },
                Description = "RGB mechanical keyboard with customizable switches and keycaps.",
                ImageFile = "keyboard.jpg",
                Price = 129.99m,
            },
            new Product()
            {
                Id = new Guid("e3f2a1b4-6c5d-4e7f-8a9b-0c1d2e3f4a5b"),
                Name = "Smartwatch",
                Category = new List<string>() { "Electronics", "Wearable" },
                Description =
                    "Feature-packed smartwatch with heart rate monitor and fitness tracking.",
                ImageFile = "smartwatch.jpg",
                Price = 199.99m,
            },
            new Product()
            {
                Id = new Guid("a4b5c6d7-8e9f-0a1b-2c3d-4e5f6a7b8c9d"),
                Name = "4K Monitor",
                Category = new List<string>() { "Electronics", "Displays" },
                Description = "Ultra HD 4K monitor with HDR support and vibrant colors.",
                ImageFile = "monitor.jpg",
                Price = 499.99m,
            },
            new Product()
            {
                Id = new Guid("f1e2d3c4-b5a6-7c8d-9e0f-1a2b3c4d5e6f"),
                Name = "External SSD",
                Category = new List<string>() { "Electronics", "Storage" },
                Description = "Fast and reliable external SSD with USB-C connectivity.",
                ImageFile = "ssd.jpg",
                Price = 179.99m,
            },
            new Product()
            {
                Id = new Guid("c9d8e7f6-a5b4-3c2d-1e0f-9a8b7c6d5e4f"),
                Name = "Wireless Mouse",
                Category = new List<string>() { "Electronics", "Accessories" },
                Description = "Ergonomic wireless mouse with adjustable DPI and long battery life.",
                ImageFile = "mouse.jpg",
                Price = 49.99m,
            },
            new Product()
            {
                Id = new Guid("b3c2d1e0-f9a8-7c6d-5e4f-3a2b1c0d9e8f"),
                Name = "Bluetooth Speaker",
                Category = new List<string>() { "Electronics", "Audio" },
                Description = "Portable Bluetooth speaker with deep bass and waterproof design.",
                ImageFile = "speaker.jpg",
                Price = 89.99m,
            },
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
                Id = new Guid("a1d5c3e2-5b6f-4d9c-a765-2e3b7f9d9f1a"),
                Name = "Smartphone",
                Category = new List<string>() { "Electronics", "Mobile" },
                Description =
                    "Latest smartphone with a high-resolution display and advanced camera system.",
                ImageFile = "smartphone.jpg",
                Price = 999.99m,
            },
            new Product()
            {
                Id = new Guid("f5a6c8d9-7e8b-4d3a-9f2c-6a5e7b8d9f0e"),
                Name = "Gaming Laptop",
                Category = new List<string>() { "Electronics", "Computers", "Gaming" },
                Description =
                    "Powerful gaming laptop with RGB lighting and high refresh rate display.",
                ImageFile = "gaming_laptop.jpg",
                Price = 1800.00m,
            },
            new Product()
            {
                Id = new Guid("b2c3d4e5-6f7a-4b8c-9d0e-1a2b3c4d5e6f"),
                Name = "Wireless Headphones",
                Category = new List<string>() { "Electronics", "Audio" },
                Description = "Noise-canceling wireless headphones with long battery life.",
                ImageFile = "headphones.jpg",
                Price = 249.99m,
            },
            new Product()
            {
                Id = new Guid("d9e8f7a6-5b4c-3d2e-1f0a-9b8c7d6e5f4a"),
                Name = "Mechanical Keyboard",
                Category = new List<string>() { "Electronics", "Accessories" },
                Description = "RGB mechanical keyboard with customizable switches and keycaps.",
                ImageFile = "keyboard.jpg",
                Price = 129.99m,
            },
            new Product()
            {
                Id = new Guid("e3f2a1b4-6c5d-4e7f-8a9b-0c1d2e3f4a5b"),
                Name = "Smartwatch",
                Category = new List<string>() { "Electronics", "Wearable" },
                Description =
                    "Feature-packed smartwatch with heart rate monitor and fitness tracking.",
                ImageFile = "smartwatch.jpg",
                Price = 199.99m,
            },
            new Product()
            {
                Id = new Guid("a4b5c6d7-8e9f-0a1b-2c3d-4e5f6a7b8c9d"),
                Name = "4K Monitor",
                Category = new List<string>() { "Electronics", "Displays" },
                Description = "Ultra HD 4K monitor with HDR support and vibrant colors.",
                ImageFile = "monitor.jpg",
                Price = 499.99m,
            },
            new Product()
            {
                Id = new Guid("f1e2d3c4-b5a6-7c8d-9e0f-1a2b3c4d5e6f"),
                Name = "External SSD",
                Category = new List<string>() { "Electronics", "Storage" },
                Description = "Fast and reliable external SSD with USB-C connectivity.",
                ImageFile = "ssd.jpg",
                Price = 179.99m,
            },
            new Product()
            {
                Id = new Guid("c9d8e7f6-a5b4-3c2d-1e0f-9a8b7c6d5e4f"),
                Name = "Wireless Mouse",
                Category = new List<string>() { "Electronics", "Accessories" },
                Description = "Ergonomic wireless mouse with adjustable DPI and long battery life.",
                ImageFile = "mouse.jpg",
                Price = 49.99m,
            },
            new Product()
            {
                Id = new Guid("b3c2d1e0-f9a8-7c6d-5e4f-3a2b1c0d9e8f"),
                Name = "Bluetooth Speaker",
                Category = new List<string>() { "Electronics", "Audio" },
                Description = "Portable Bluetooth speaker with deep bass and waterproof design.",
                ImageFile = "speaker.jpg",
                Price = 89.99m,
            },
            new Product()
            {
                Id = new Guid("d7f8e9a6-5b4c-3d2e-1f0a-9b8c7d6e5f4a"),
                Name = "Tablet",
                Category = new List<string>() { "Electronics", "Tablets" },
                Description =
                    "Lightweight and powerful tablet with a high-resolution display and stylus support.",
                ImageFile = "tablet.jpg",
                Price = 799.99m,
            },
            new Product()
            {
                Id = new Guid("e4f5d6c7-b8a9-0c1d-2e3f-4a5b6c7d8e9f"),
                Name = "Gaming Chair",
                Category = new List<string>() { "Furniture", "Gaming" },
                Description = "Ergonomic gaming chair with adjustable armrests and lumbar support.",
                ImageFile = "gaming_chair.jpg",
                Price = 299.99m,
            },
            new Product()
            {
                Id = new Guid("c6d7e8f9-a5b4-3c2d-1e0f-9a8b7c6d5e4f"),
                Name = "Graphics Tablet",
                Category = new List<string>() { "Electronics", "Drawing" },
                Description =
                    "Professional graphics tablet with pressure-sensitive pen and customizable buttons.",
                ImageFile = "graphics_tablet.jpg",
                Price = 349.99m,
            },
            new Product()
            {
                Id = new Guid("b1c2d3e4-f5a6-7c8d-9e0f-1a2b3c4d5e6f"),
                Name = "VR Headset",
                Category = new List<string>() { "Electronics", "Gaming" },
                Description =
                    "Immersive VR headset with motion tracking and high refresh rate display.",
                ImageFile = "vr_headset.jpg",
                Price = 599.99m,
            },
            new Product()
            {
                Id = new Guid("f9e8d7c6-a5b4-3c2d-1e0f-9a8b7c6d5e4f"),
                Name = "Drone",
                Category = new List<string>() { "Electronics", "Cameras" },
                Description = "Advanced drone with 4K camera, GPS, and obstacle avoidance system.",
                ImageFile = "drone.jpg",
                Price = 999.99m,
            },
            new Product()
            {
                Id = new Guid("a7b8c9d0-e1f2-3c4d-5e6f-7a8b9c0d1e2f"),
                Name = "E-Reader",
                Category = new List<string>() { "Electronics", "Reading" },
                Description =
                    "E-ink display e-reader with adjustable lighting and weeks of battery life.",
                ImageFile = "e_reader.jpg",
                Price = 129.99m,
            },
        };
}
