using Bogus;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Seeders;

public static class OrderSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Orders.AnyAsync())
            return;

        var userIds = await context.Users.Select(u => u.Id).ToListAsync();

        if (!userIds.Any())
            return;

        var orderFaker = new Faker<Order>()
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.OrderNote, f => f.Lorem.Sentence())
            .RuleFor(o => o.OrderNumber, f => f.Commerce.Ean13())
            .RuleFor(o => o.SubTotal, f => f.Random.Decimal(10, 1000))
            .RuleFor(o => o.DiscountAmount, f => f.Random.Decimal(0, 50))
            .RuleFor(o => o.ShippingAmount, f => f.Random.Decimal(5, 20))
            .RuleFor(o => o.TaxAmount, f => f.Random.Decimal(1, 100))
            .RuleFor(o => o.Status, f => f.PickRandom<OrderStatus>())
            .RuleFor(o => o.CreatedAt, f => f.Date.PastOffset(1).UtcDateTime)
            .RuleFor(o => o.UpdatedAt, (_, o) => o.CreatedAt);

        var orders = new List<Order>();
        foreach (var userId in userIds)
        {
            var userOrders = orderFaker.Generate(Random.Shared.Next(1, 5));
            userOrders.ForEach(o => 
            {
                o.UserId = userId;
                o.TotalAmount = o.SubTotal + o.TaxAmount + o.ShippingAmount - o.DiscountAmount;
            });
            orders.AddRange(userOrders);
        }

        await context.Orders.AddRangeAsync(orders);
        await context.SaveChangesAsync();
    }
}

