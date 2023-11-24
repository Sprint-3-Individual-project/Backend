using ShopProduct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebshopBackend.DTOs
{
    public class ProductDTO
    {
        public ProductDTO(int productid, string name, decimal price, int stock, string fotourl)
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

        //TODO: Todto functie maken
        public static ProductDTO CastProduct(Product product)
        {
            return new ProductDTO(product.Productid, product.Name, product.Price, product.Stock, product.FotoUrl);
        }
    }
}
