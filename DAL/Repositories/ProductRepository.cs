using DAL.Entities;
using ShopProduct;
using ShopProduct.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopBackend.Exceptions;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductData _context;
        private List<Product> _products = new List<Product>();
        private Product _product;

        public ProductRepository(ProductData context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            List<ProductEntity> entities = _context.Product.ToList();

            foreach (ProductEntity entity in entities)
            {
                Product product = new Product(entity.productid, entity.name, entity.price, entity.stock, entity.fotourl);
                _products.Add(product);
            }
            return _products;
        }
        public Product? GetProductByID(int id)
        {
            ProductEntity entity = _context.Product.Where(product => product.productid == id).FirstOrDefault();
            if (entity != null)
            {
                _product = new Product(entity.productid, entity.name, entity.price, entity.stock, entity.fotourl);
            }
            else
            {
                throw new ProductNotFoundException();
            }
            return _product;
        }
    }
}

//.Select(dto => new ProductEntity(dto));
