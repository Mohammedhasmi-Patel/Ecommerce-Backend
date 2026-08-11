
using Ecommerce.API.Database;
using Ecommerce.API.Entities;
using Ecommerce.API.Enum;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Seeders;

public static class RoleSeeder
{
    public static async Task SeedAsync(AppDbContext appDbContext)
    {
        if(await appDbContext.AppRoles.AnyAsync()) return;
        var roles = System.Enum.GetValues<UserRoleEnum>();
        var appRoles = roles.Select(r => new AppRole()
        {
            Id = Guid.NewGuid(),
            Name = r.ToString(),
            Description = r.ToString(),
            NormalizedName = r.ToString().ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        });

        await appDbContext.Roles.AddRangeAsync(appRoles);
        await appDbContext.SaveChangesAsync();

    }

}
