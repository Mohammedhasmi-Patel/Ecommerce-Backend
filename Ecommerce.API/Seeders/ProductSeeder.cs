using Bogus;
using Ecommerce.API.Database;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Seeders;

public static class ProductSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Products.AnyAsync()) return;

        var productFaker = new Faker<Product>()
                                .RuleFor(p => p.Id, f => Guid.NewGuid())
                                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                                .RuleFor(p => p.Slug, f => f.Lorem.Slug())
                                .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
                                .RuleFor(p => p.OriginalPrice, f => f.Random.Decimal(1, 1000))
                                .RuleFor(p => p.SellPrice, f => f.Random.Decimal(1, 1000))
                                .RuleFor(p => p.StockQuantity, f => f.Random.Int(1, 1000))
                                .RuleFor(p => p.CreatedAt, DateTime.UtcNow)
                                .RuleFor(p => p.UpdatedAt, DateTime.UtcNow);

        var products = productFaker.Generate(100);

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
