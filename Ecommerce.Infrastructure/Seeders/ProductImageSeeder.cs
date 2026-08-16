using Bogus;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Seeders;

public static class ProductImageSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.ProductImages.AnyAsync()) return;

        var productIds = await context.Products.Select(p => p.Id).ToListAsync();
        if (productIds.Count == 0) return;

        var productImages = new List<ProductImage>();
        var random = new Random();
        var faker = new Faker();

        foreach (var productId in productIds)
        {
            // Generate 2 to 4 images per product
            int imageCount = random.Next(2, 5); 

            for (int i = 0; i < imageCount; i++)
            {
                var fileName = faker.System.FileName("jpg");
                
                productImages.Add(new ProductImage
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    FileName = fileName,
                    FilePath = faker.Image.PicsumUrl(), // Dummy image URL
                    FileExtension = ".jpg",
                    FileSize = faker.Random.Long(10240, 5120000), // 10KB to 5MB
                    MimeType = "image/jpeg",
                    SortOrder = i,
                    IsPrimary = i == 0
                });
            }
        }

        await context.ProductImages.AddRangeAsync(productImages);
        await context.SaveChangesAsync();
    }
}

