using Bogus;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Seeders;

public static class UserSeeder
{
    public async static Task SeedAsync(AppDbContext dbContext,UserManager<AppUser> userManager)
    {
        if (await dbContext.Users.AnyAsync()) return;
        
        var userFaker = new Faker<AppUser>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
            .RuleFor(x => x.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(x => x.Avatar, f => f.Image.PicsumUrl())
            .RuleFor(x => x.IsActive, f => true)
            .RuleFor(x => x.CreatedAt, f => DateTime.UtcNow)
            .RuleFor(x => x.UpdatedAt, f => null)
            .RuleFor(x => x.DeletedAt, f => null);

        var users = userFaker.Generate(100);

        foreach (var user in users)
        {
            var result = await userManager.CreateAsync(user, "User@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, UserRoleEnum.User.ToString());
            }
            else
            {
                Console.WriteLine($"Failed to create user {user.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}

