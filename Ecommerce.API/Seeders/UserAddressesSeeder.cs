using Bogus;
using Ecommerce.API.Database;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Seeders;

public static class UserAddressesSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.UserAddresses.AnyAsync())
            return;

        var userIds = await context.Users.Select(u => u.Id).ToListAsync();
        var countryIds = await context.Countries.Select(c => c.Id).ToListAsync();
        var stateIds = await context.States.Select(s => s.Id).ToListAsync();
        var cityIds = await context.Cities.Select(c => c.Id).ToListAsync();

        if (!userIds.Any() || !countryIds.Any() || !stateIds.Any() || !cityIds.Any())
            return;

        var addressFaker = new Faker<UserAddresses>()
            .RuleFor(ua => ua.Id, f => Guid.NewGuid())
            .RuleFor(ua => ua.FullName, f => f.Person.FullName)
            .RuleFor(ua => ua.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(ua => ua.AddressLine1, f => f.Address.StreetAddress())
            .RuleFor(ua => ua.AddressLine2, f => f.Address.SecondaryAddress())
            .RuleFor(ua => ua.CountryId, f => f.PickRandom(countryIds))
            .RuleFor(ua => ua.StateId, f => f.PickRandom(stateIds))
            .RuleFor(ua => ua.CityId, f => f.PickRandom(cityIds))
            .RuleFor(ua => ua.PostalCode, f => f.Address.ZipCode())
            .RuleFor(ua => ua.IsDefault, f => f.Random.Bool())
            .RuleFor(ua => ua.CreatedAt, f => f.Date.PastOffset(1).UtcDateTime)
            .RuleFor(ua => ua.UpdatedAt, (_, ua) => ua.CreatedAt);

        var userAddresses = new List<UserAddresses>();
        foreach (var userId in userIds)
        {
            var addresses = addressFaker.Generate(Random.Shared.Next(1, 4));
            addresses.ForEach(a => a.UserId = userId);
            
            // Ensure only one default address
            if (addresses.Any())
            {
                addresses.ForEach(a => a.IsDefault = false);
                addresses.First().IsDefault = true;
            }

            userAddresses.AddRange(addresses);
        }

        await context.UserAddresses.AddRangeAsync(userAddresses);
        await context.SaveChangesAsync();
    }
}
