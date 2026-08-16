using Bogus;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Seeders;

public static class WishlistSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Wishlists.AnyAsync())
            return;

        var userIds = await context.Users.Select(u => u.Id).ToListAsync();

        if (!userIds.Any())
            return;

        var wishlistFaker = new Faker<Wishlist>()
            .RuleFor(w => w.Id, f => Guid.NewGuid())
            .RuleFor(w => w.CreatedAt, f => f.Date.PastOffset(1).UtcDateTime)
            .RuleFor(w => w.UpdatedAt, (_, w) => w.CreatedAt);

        var wishlists = new List<Wishlist>();
        foreach (var userId in userIds)
        {
            var wishlist = wishlistFaker.Generate();
            wishlist.UserId = userId;
            wishlists.Add(wishlist);
        }

        await context.Wishlists.AddRangeAsync(wishlists);
        await context.SaveChangesAsync();
    }
}

