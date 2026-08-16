using Bogus;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Seeders;

public static class CouponSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Coupons.AnyAsync())
            return;

        var couponFaker = new Faker<Coupon>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Code, f => f.Random.AlphaNumeric(8).ToUpper())
            .RuleFor(c => c.Description, f => f.Lorem.Sentence())
            .RuleFor(c => c.DiscountType, f => f.PickRandom("Percentage", "FixedAmount"))
            .RuleFor(c => c.DiscountValue, f => f.Random.Decimal(5, 50))
            .RuleFor(c => c.StartsAt, f => f.Date.PastOffset(1).UtcDateTime)
            .RuleFor(c => c.ExpiresAt, f => f.Date.FutureOffset(1).UtcDateTime)
            .RuleFor(c => c.IsActive, f => f.Random.Bool())
            .RuleFor(c => c.CreatedAt, f => f.Date.PastOffset(1).UtcDateTime)
            .RuleFor(c => c.UpdatedAt, (_, c) => c.CreatedAt);

        var coupons = couponFaker.Generate(20);

        await context.Coupons.AddRangeAsync(coupons);
        await context.SaveChangesAsync();
    }
}

