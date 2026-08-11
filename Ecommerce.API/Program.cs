using Ecommerce.API.Extension;
using Ecommerce.API.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureProjectServices(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
