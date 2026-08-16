using Ecommerce.API.Extension;
using Ecommerce.API.Middleware;
using Ecommerce.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureProjectServices(builder.Configuration);

var app = builder.Build();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
}

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
