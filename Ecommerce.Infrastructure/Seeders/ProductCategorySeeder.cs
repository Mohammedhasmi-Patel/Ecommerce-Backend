using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Seeders;

public static class ProductCategorySeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.ProductCategories.AnyAsync()) return;

        var productIds = await context.Products.Select(p => p.Id).ToListAsync();
        var categoryIds = await context.Categories.Select(c => c.Id).ToListAsync();

        if (productIds.Count == 0 || categoryIds.Count == 0) return;

        var productCategories = new List<ProductCategory>();
        var random = new Random();

        foreach (var productId in productIds)
        {
            // Pick 1 to 3 random categories for each product
            var selectedCategoryIds = categoryIds.OrderBy(x => random.Next()).Take(random.Next(1, 4)).ToList();

            foreach (var categoryId in selectedCategoryIds)
            {
                productCategories.Add(new ProductCategory
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    CategoryId = categoryId
                });
            }
        }

        await context.ProductCategories.AddRangeAsync(productCategories);
        await context.SaveChangesAsync();
    }
}

