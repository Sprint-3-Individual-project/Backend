using DAL;
using ShopProduct.Interfaces;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ShopProduct;
using DAL.Repositories;
using WebshopBackend.Config;
using User.Interfaces;
using User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration["AllowedOrigins"];

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("_webshop_frontend",
        builder =>
        {
            builder
            .WithOrigins(settings?.Split(",") ?? new string[] { "" })
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddScoped<IProductData>(x => new ProductData(ConString: builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserManager, UserManager>();
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

using (IServiceScope serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    ProductData? context = serviceScope.ServiceProvider.GetService<ProductData>();
    try
    {
        context?.Database.EnsureCreated();
    }
    catch(Exception ex)
    {
    }
}

app.Run();

namespace WebshopBackend
{
    public class WebshopBackendProgram { }
}