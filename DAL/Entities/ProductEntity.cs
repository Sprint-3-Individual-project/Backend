using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopProduct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProductEntity 
    {
        public ProductEntity(int productid, string name, decimal price, int stock, string fotourl)
        {
            this.productid = productid;
            this.name = name;
            this.price = price;
            this.stock = stock;
            this.fotourl = fotourl;
        }

        [Key]
        public int productid { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
        public string fotourl { get; set; }

        public static Product ToProduct(ProductEntity entity)
        {
            return new Product(entity.productid, entity.name, entity.price, entity.stock, entity.fotourl);
        }
    }
}
