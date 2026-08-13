using Bogus;
using Ecommerce.API.Database;
using Ecommerce.API.Entities;
using Ecommerce.API.Enum;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Seeders;

public static class PaymentSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Payments.AnyAsync())
            return;

        var orders = await context.Orders
            .Select(o => new { o.Id, o.TotalAmount })
            .ToListAsync();

        if (!orders.Any())
            return;

        var paymentFaker = new Faker<Payment>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Provider, f => f.PickRandom("Stripe", "PayPal", "Razorpay"))
            .RuleFor(p => p.PaymentMethod, f => f.PickRandom("CreditCard", "DebitCard", "UPI", "BankTransfer"))
            .RuleFor(p => p.ProviderPaymentId, f => f.Random.AlphaNumeric(20))
            .RuleFor(p => p.Currency, f => "USD")
            .RuleFor(p => p.Status, f => f.PickRandom<PaymentStatus>())
            .RuleFor(p => p.PaidAt, f => f.Date.PastOffset(1).UtcDateTime)
            .RuleFor(p => p.CreatedAt, f => f.Date.PastOffset(1).UtcDateTime)
            .RuleFor(p => p.UpdatedAt, (_, p) => p.CreatedAt);

        var payments = new List<Payment>();
        foreach (var order in orders)
        {
            var payment = paymentFaker.Generate();
            payment.OrderId = order.Id;
            payment.Amount = order.TotalAmount;
            payments.Add(payment);
        }

        await context.Payments.AddRangeAsync(payments);
        await context.SaveChangesAsync();
    }
}
