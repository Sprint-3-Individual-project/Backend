using DAL;
using ShopProduct.Interfaces;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ShopProduct;
using DAL.Repositories;
using WebshopBackend.Config;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.GetSection("AllowedOrigins");

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("_webshop_frontend",
        builder =>
        {
            builder.WithOrigins(settings.Value.Split(",")).AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddScoped<IProductData>(x => new ProductData(ConString: builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddDbContext<ProductData>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"), (b) =>
    {
        b.MigrationsAssembly("WebshopBackend");
    });
    Console.WriteLine(builder.Configuration.GetConnectionString("Default"));
}, ServiceLifetime.Transient);
builder.Services.AddSwaggerGen();

var app = builder.Build();


//builder.Services.AddDbContext<ProductData>(options => options.)

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("_webshop_frontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace WebshopBackend
{
    public class WebshopBackendProgram { }
}