using Bogus;
using Ecommerce.API.Database;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Seeders;

public static class OrderItemSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.OrderItems.AnyAsync())
            return;

        var orderIds = await context.Orders.Select(o => o.Id).ToListAsync();
        
        var products = await context.Products
            .Select(p => new { p.Id, p.Name, p.SellPrice })
            .ToListAsync();

        if (!orderIds.Any() || !products.Any())
            return;

        var orderItemFaker = new Faker<OrderItem>()
            .RuleFor(oi => oi.Id, f => Guid.NewGuid())
            .RuleFor(oi => oi.Quantity, f => f.Random.Int(1, 5))
            .RuleFor(oi => oi.CreatedAt, f => f.Date.PastOffset(1).UtcDateTime)
            .RuleFor(oi => oi.UpdatedAt, (_, oi) => oi.CreatedAt);

        var orderItems = new List<OrderItem>();
        foreach (var orderId in orderIds)
        {
            var selectedProducts = products
                .OrderBy(_ => Guid.NewGuid())
                .Take(Random.Shared.Next(1, Math.Min(5, products.Count) + 1))
                .ToList();

            foreach (var product in selectedProducts)
            {
                var orderItem = orderItemFaker.Generate();
                orderItem.OrderId = orderId;
                orderItem.ProductId = product.Id;
                orderItem.ProductName = product.Name;
                orderItem.UnitPrice = product.SellPrice;
                orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;

                orderItems.Add(orderItem);
            }
        }

        await context.OrderItems.AddRangeAsync(orderItems);
        await context.SaveChangesAsync();
    }
}
