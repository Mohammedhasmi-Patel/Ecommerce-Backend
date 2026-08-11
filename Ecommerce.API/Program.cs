using Ecommerce.API.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureProjectServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
