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

        await RoleSeeder.SeedAsync(dbContext);
        await UserSeeder.SeedAsync(dbContext, userManager);
        
        await CountrySeeder.SeedAsync(dbContext);
        await StateSeeder.SeedAsync(dbContext);
        await CitySeeder.SeedAsync(dbContext);
        await UserAddressesSeeder.SeedAsync(dbContext);

        await CategorySeeder.SeedAsync(dbContext);
        await ProductSeeder.SeedAsync(dbContext);
        await ProductCategorySeeder.SeedAsync(dbContext);
        await ProductImageSeeder.SeedAsync(dbContext);

        await CouponSeeder.SeedAsync(dbContext);
        
        await CartSeeder.SeedAsync(dbContext);
        await CartItems.SeedAsync(dbContext);

        await WishlistSeeder.SeedAsync(dbContext);
        await WishlistItemSeeder.SeedAsync(dbContext);

        await OrderSeeder.SeedAsync(dbContext);
        await OrderAddressSeeder.SeedAsync(dbContext);
        await OrderItemSeeder.SeedAsync(dbContext);
        await PaymentSeeder.SeedAsync(dbContext);
    }

}
