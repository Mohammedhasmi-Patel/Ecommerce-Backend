using System.Text;
using Ecommerce.API.Configurations;
using Ecommerce.API.Database;
using Ecommerce.API.DTO;
using Ecommerce.API.Entities;
using Ecommerce.API.Interfaces;
using Ecommerce.API.RepoContracts;
using Ecommerce.API.Repositories;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Ecommerce.API.Extension;

public static class ConfigureService
{
    public static IServiceCollection ConfigureProjectServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            });
        });
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
        service.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
        });

        string databaseUrl = configuration.GetConnectionString("Default") ?? throw new Exception("Database string not found.");

        service.AddDbContext<AppDbContext>(options => options.UseNpgsql(databaseUrl));

        service.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        service.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));
        JwtConfiguration jwtConfiguration = configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>()!;
        service.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtConfiguration.Secret))
            };
        });
        service.AddScoped<IAuthService, AuthService>();
        service.AddScoped<ITokenService, TokenService>();
        service.AddScoped<IAppUserRepository, AppUserRepository>();
        service.AddScoped<IStorageService, StorageService>();
        service.AddScoped<IProductService, ProductService>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<ICartRepository, CartRepository>();
        service.AddScoped<ICartService, CartService>();

        return service;
    }

}
