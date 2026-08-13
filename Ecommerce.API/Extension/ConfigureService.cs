using Ecommerce.API.Database;
using Ecommerce.API.DTO;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Extension;

public static class ConfigureService
{
    public static IServiceCollection ConfigureProjectServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddControllers();
        service.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var firstError = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault() ?? "Validation failed";

                var errorRes = ApiResponse<object>.ErrorResponse(firstError, 400);
                return new BadRequestObjectResult(errorRes);
            };
        });

        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen();

        string databaseUrl = configuration.GetConnectionString("Default") ?? throw new Exception("Database string not found.");

        service.AddDbContext<AppDbContext>(options => options.UseNpgsql(databaseUrl));

        service.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        return service;
    }

}
