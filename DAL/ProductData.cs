using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopProduct.Interfaces;
using ShopProduct;
using Microsoft.Extensions.Options;
using DAL.Entities;

namespace DAL
{ 
    public class ProductData : DbContext
    {

        //IdentityDbContext
        public ProductData(DbContextOptions<ProductData> options) : base(options) { }

        public DbSet<ProductEntity> Product { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        @"Data Source=GIJS;Initial Catalog=S3Webshop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //}
    }
}
