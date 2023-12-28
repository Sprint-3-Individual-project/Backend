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
        public DbSet<AccountEntity> Account { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}
    }
}
