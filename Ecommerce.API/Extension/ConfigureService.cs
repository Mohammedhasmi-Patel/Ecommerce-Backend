using Ecommerce.API.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Extension;

public static class ConfigureService
{
    public static IServiceCollection ConfigureProjectServices(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddControllers();
        string databaseUrl = configuration.GetConnectionString("Default") ?? throw new Exception("Database string not found.");

        service.AddDbContext<AppDbContext>(options => options.UseNpgsql(databaseUrl));
        return service;
    }

}
