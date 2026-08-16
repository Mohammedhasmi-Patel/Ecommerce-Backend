using Bogus;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Seeders;

public static class CartItems
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.CartItems.AnyAsync())
            return;

        var cartIds = await context.Carts
                .Select(c => c.Id)
                .ToListAsync();

        var productIds = await context.Products
                .Select(p => p.Id)
                .ToListAsync();


        if (cartIds.Count() == 0 || productIds.Count() == 0) return;

        var cartItemFaker = new Faker<CartItem>()
            .RuleFor(ci => ci.Id, f => Guid.NewGuid())
            .RuleFor(ci => ci.CartId, (_, ci) => ci.CartId)
            .RuleFor(ci => ci.ProductId, (_, ci) => ci.ProductId)
            .RuleFor(ci => ci.Quantity, f => f.Random.Int(1, 10))
            .RuleFor(ci => ci.CreatedAt, f => f.Date.PastOffset(1).UtcDateTime)
            .RuleFor(ci => ci.UpdatedAt, (_, ci) => ci.CreatedAt);

        var cartItems = new List<CartItem>();

        foreach (var cartId in cartIds)
        {
            var selectedProductIds = productIds
                .OrderBy(_ => Guid.NewGuid())
                .Take(Random.Shared.Next(
                    1,
                    Math.Min(5, productIds.Count) + 1))
                .ToList();

            foreach (var productId in selectedProductIds)
            {
                var cartItem = cartItemFaker.Generate();

                cartItem.CartId = cartId;
                cartItem.ProductId = productId;

                cartItems.Add(cartItem);
            }
        }

        await context.CartItems.AddRangeAsync(cartItems);
        await context.SaveChangesAsync();

    }
}

