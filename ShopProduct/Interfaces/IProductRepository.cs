using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProduct.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product? GetProductByID(int id);
        void UpdateProductPrice(Product product, decimal newPrice);
        Task AddProduct(Product product);
    }
}
