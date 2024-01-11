using DAL;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopProduct;
using ShopProduct.Interfaces;
using System.Text;
using User;
using User.Interfaces;
using WebshopBackend.Hubs;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration["AllowedOrigins"];

//Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("_webshop_frontend",
       builder =>
        {
            builder
            .WithOrigins(settings?.Split(",") ?? new string[] { "" })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
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
builder.Services.AddSignalR(options =>
{
    options.KeepAliveInterval = TimeSpan.FromMinutes(2);
});

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

//app.Use(async (context, next) =>
//{
//    if (context.Request.Path == "/ws")
//    {
//        if (context.WebSockets.IsWebSocketRequest)
//        {
//            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
//            var message = "hello world";
//            var bytes = Encoding.UTF8.GetBytes(message);
//            var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
//            await webSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
//        }
//        else
//        {
//            context.Response.StatusCode = StatusCodes.Status400BadRequest;
//        }
//    }
//    else
//    {
//        await next(context);
//    }

//});
//app.Map("/ws", async context =>
//{
//    if (context.WebSockets.IsWebSocketRequest)
//    {
//        using var ws = await context.WebSockets.AcceptWebSocketAsync();
//        var message = "hello world";
//        var bytes = Encoding.UTF8.GetBytes(message);
//        var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
//        await ws.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
//    }
//    else
//    {
//        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
//    }
//});



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

app.MapHub<DefaultHub>("/ws");

using (IServiceScope serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    ProductData? context = serviceScope.ServiceProvider.GetService<ProductData>();
    try
    {
        context?.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
    }
}

app.Run();

namespace WebshopBackend
{
    public class WebshopBackendProgram { }
}