using Ecommerce.Application.Interfaces;
using Ecommerce.Application.RepoContracts;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Infrastructure.Security;
using Ecommerce.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseUrl = configuration.GetConnectionString("Default") ?? throw new Exception("Database string not found.");
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(databaseUrl));

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repositories
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();
        services.AddScoped<ICountriesRepository, CountriesRepository>();
        services.AddScoped<IStatesRepository, StatesRepository>();
        services.AddScoped<ICitiesRepository, CitiesRepository>();

        // Infra services
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
