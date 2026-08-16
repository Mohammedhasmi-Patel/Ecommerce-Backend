using Bogus;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Seeders;

public static class CategorySeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Categories.AnyAsync()) return;
         var parentCategories = new Faker<Category>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Commerce.Department())
            .RuleFor(c => c.Slug, (f, c) => c.Name.ToLower().Replace(" ", "-"))
            .RuleFor(c => c.Description, f => f.Lorem.Sentence())
            .RuleFor(c => c.SortOrder, f => f.Random.Int(1, 100))
            .RuleFor(c => c.IsFeatured, f => f.Random.Bool())
            .RuleFor(c => c.ParentId, _ => null)
            .Generate(30);

            var childCategories = new Faker<Category>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => f.Commerce.ProductAdjective())
                .RuleFor(c => c.Slug, (f, c) => c.Name.ToLower().Replace(" ", "-"))
                .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                .RuleFor(c => c.SortOrder, f => f.Random.Int(1, 100))
                .RuleFor(c => c.IsFeatured, f => f.Random.Bool())
                .RuleFor(c => c.ParentId, f => f.PickRandom(parentCategories).Id)
                .Generate(100);

            var allCategories = parentCategories.Concat(childCategories);

            await context.Categories.AddRangeAsync(allCategories);
            await context.SaveChangesAsync();

    }
}

