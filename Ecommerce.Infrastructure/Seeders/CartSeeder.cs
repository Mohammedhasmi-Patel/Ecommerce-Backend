using Bogus;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Seeders;

public static class CartSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Carts.AnyAsync()) return;

        var userIds = await context.Users.Select(u => u.Id).ToListAsync();
        if (userIds.Count == 0) return;
        // Pick exactly 30% of unique user IDs
        var selectedUserIds = new Faker().PickRandom(userIds, Math.Max(1, userIds.Count * 30 / 100)).ToList();

        var cartFaker = new Faker<Cart>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.CreatedAt, f => f.Date.Past(1).ToUniversalTime())
                .RuleFor(c => c.UpdatedAt, (f, c) => c.CreatedAt);

        var carts = selectedUserIds.Select(userId => {
            var cart = cartFaker.Generate();
            cart.UserId = userId;
            return cart;
        }).ToList();

        await context.Carts.AddRangeAsync(carts);
        await context.SaveChangesAsync();
    }
}

