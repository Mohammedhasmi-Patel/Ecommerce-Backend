using System.Text.Json;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Seeders;

public static class CountrySeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Countries.AnyAsync()) return;

        var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "JsonFiles", "countries.json");
        if (!File.Exists(path)) return;

        var json = await File.ReadAllTextAsync(path);
        
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var jsonCountries = JsonSerializer.Deserialize<List<JsonCountry>>(json, options);

        if (jsonCountries != null && jsonCountries.Any())
        {
            var countries = jsonCountries.Select(c => new Country
            {
                Id = Guid.NewGuid(),
                Name = c.Name,
                Code = c.Iso2,
                PhoneCode = c.Phonecode,
                CurrencyCode = c.Currency ?? string.Empty,
                IsActive = true
            }).ToList();

            await context.Countries.AddRangeAsync(countries);
            await context.SaveChangesAsync();
        }
    }

    private class JsonCountry
    {
        public string Name { get; set; } = null!;
        public string Iso2 { get; set; } = null!;
        public string Phonecode { get; set; } = null!;
        public string? Currency { get; set; }
    }
}

