using Bogus;
using Ecommerce.API.Database;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Seeders;

public static class WishlistItemSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.WishlistItems.AnyAsync())
            return;

        var wishlistIds = await context.Wishlists.Select(w => w.Id).ToListAsync();
        var productIds = await context.Products.Select(p => p.Id).ToListAsync();

        if (wishlistIds.Count == 0 || productIds.Count == 0)
            return;

        var wishlistItemFaker = new Faker<WishlistItem>()
            .RuleFor(wi => wi.Id, f => Guid.NewGuid())
            .RuleFor(wi => wi.CreatedAt, f => f.Date.PastOffset(1).UtcDateTime);

        var wishlistItems = new List<WishlistItem>();
        foreach (var wishlistId in wishlistIds)
        {
            var selectedProductIds = productIds
                .OrderBy(_ => Guid.NewGuid())
                .Take(Random.Shared.Next(0, Math.Min(10, productIds.Count)))
                .ToList();

            foreach (var productId in selectedProductIds)
            {
                var wishlistItem = wishlistItemFaker.Generate();
                wishlistItem.WishlistId = wishlistId;
                wishlistItem.ProductId = productId;
                wishlistItems.Add(wishlistItem);
            }
        }

        await context.WishlistItems.AddRangeAsync(wishlistItems);
        await context.SaveChangesAsync();
    }
}
