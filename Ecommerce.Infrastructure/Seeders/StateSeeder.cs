using System.Text.Json;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Seeders;

public static class StateSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.States.AnyAsync()) return;
        var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "JsonFiles", "states.json");
        if (!File.Exists(path)) return;
        var json = await File.ReadAllTextAsync(path);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var jsonStates = JsonSerializer.Deserialize<List<JsonState>>(json, options);

        if (jsonStates != null && jsonStates.Any())
        {
            var countryMap = await context.Countries.ToDictionaryAsync(c => c.Code, c => c.Id);
            var states = new List<State>();

            foreach (var s in jsonStates)
            {
                if (countryMap.TryGetValue(s.Country_code, out var countryId))
                {
                    states.Add(new State
                    {
                        Id = Guid.NewGuid(),
                        Name = s.Name,
                        Code = s.Iso2,
                        CountryId = countryId,
                        IsActive = true
                    });
                }
            }
            await context.States.AddRangeAsync(states);
            await context.SaveChangesAsync();
        }
    }

    private class JsonState
    {
        public string Name { get; set; } = null!;
        public string Iso2 { get; set; } = null!;
        public string Country_code { get; set; } = null!;
    }
}

