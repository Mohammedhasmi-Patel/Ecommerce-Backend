using Bogus;
using Ecommerce.API.Database;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Seeders;

public static class OrderAddressSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.OrderAddresses.AnyAsync())
            return;

        var orderIds = await context.Orders.Select(o => o.Id).ToListAsync();

        if (!orderIds.Any())
            return;

        var orderAddressFaker = new Faker<OrderAddress>()
            .RuleFor(oa => oa.Id, f => Guid.NewGuid())
            .RuleFor(oa => oa.FullName, f => f.Person.FullName)
            .RuleFor(oa => oa.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(oa => oa.AddressLine1, f => f.Address.StreetAddress())
            .RuleFor(oa => oa.AddressLine2, f => f.Address.SecondaryAddress())
            .RuleFor(oa => oa.CountryName, f => f.Address.Country())
            .RuleFor(oa => oa.StateName, f => f.Address.State())
            .RuleFor(oa => oa.CityName, f => f.Address.City())
            .RuleFor(oa => oa.PostalCode, f => f.Address.ZipCode())
            .RuleFor(oa => oa.CreatedAt, f => f.Date.PastOffset(1).UtcDateTime)
            .RuleFor(oa => oa.UpdatedAt, (_, oa) => oa.CreatedAt);

        var orderAddresses = new List<OrderAddress>();
        foreach (var orderId in orderIds)
        {
            var orderAddress = orderAddressFaker.Generate();
            orderAddress.OrderId = orderId;
            orderAddresses.Add(orderAddress);
        }

        await context.OrderAddresses.AddRangeAsync(orderAddresses);
        await context.SaveChangesAsync();
    }
}
