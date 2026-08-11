using Bogus;
using Ecommerce.API.Database;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.API.Seeders;

public static class DatabaseSeeder
{
    public async static Task SeedAsync(IServiceProvider serviceProvider)
    {

        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        // Roles
        await RoleSeeder.SeedAsync(dbContext);
        await UserSeeder.SeedAsync(dbContext, userManager);

    }

}
