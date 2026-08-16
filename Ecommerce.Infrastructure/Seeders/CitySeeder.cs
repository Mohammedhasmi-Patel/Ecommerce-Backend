using System.Text.Json;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Seeders;

public static class CitySeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Cities.AnyAsync()) return;
        
        var citiesPath = Path.Combine(AppContext.BaseDirectory, "Seeders", "JsonFiles", "cities.json");
        var statesPath = Path.Combine(AppContext.BaseDirectory, "Seeders", "JsonFiles", "states.json");

        if (!File.Exists(citiesPath) || !File.Exists(statesPath)) return;

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        
        var statesJson = await File.ReadAllTextAsync(statesPath);
        var jsonStates = JsonSerializer.Deserialize<List<JsonState>>(statesJson, options);
        var stateMap = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);
        
        var dbStates = await context.States.ToListAsync();
        var dbCountries = await context.Countries.ToDictionaryAsync(c => c.Code, c => c.Id, StringComparer.OrdinalIgnoreCase);

        if (jsonStates != null)
        {
            foreach (var js in jsonStates)
            {
                if (!dbCountries.TryGetValue(js.Country_code, out var countryId)) continue;

                var dbState = dbStates.FirstOrDefault(s => s.Code == js.Iso2 && s.CountryId == countryId);
                
                if (dbState != null)
                {
                    if (!string.IsNullOrEmpty(js.Fips_code))
                    {
                        stateMap[$"{js.Country_code}_{js.Fips_code}"] = dbState.Id;
                    }
                    if (!string.IsNullOrEmpty(js.Iso2))
                    {
                        stateMap[$"{js.Country_code}_{js.Iso2}"] = dbState.Id;
                    }
                }
            }
        }

        var citiesJson = await File.ReadAllTextAsync(citiesPath);
        var jsonCities = JsonSerializer.Deserialize<List<JsonCity>>(citiesJson, options);

        if (jsonCities != null && jsonCities.Any())
        {
            var cities = new List<City>();
            foreach (var c in jsonCities)
            {
                var key = $"{c.Country}_{c.Admin1}";
                if (stateMap.TryGetValue(key, out var stateId))
                {
                    cities.Add(new City
                    {
                        Id = Guid.NewGuid(),
                        Name = c.Name,
                        Code = c.Name,
                        StateId = stateId,
                        IsActive = true
                    });
                }
            }

            int batchSize = 10000;
            for (int i = 0; i < cities.Count; i += batchSize)
            {
                var batch = cities.Skip(i).Take(batchSize).ToList();
                await context.Cities.AddRangeAsync(batch);
                await context.SaveChangesAsync();
            }
        }
    }

    private class JsonState
    {
        public string Iso2 { get; set; } = null!;
        public string Country_code { get; set; } = null!;
        public string? Fips_code { get; set; }
    }

    private class JsonCity
    {
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Admin1 { get; set; } = null!;
    }
}

