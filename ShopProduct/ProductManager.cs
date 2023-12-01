using ShopProduct.Interfaces;
using System.Runtime.CompilerServices;

namespace ShopProduct
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository _productRepository;
        private List<Product> _products;
        private Product _product;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            _products = (List<Product>)_productRepository.GetAllProducts();  // Tijd logica zetten

            CheckDiscount(_products);

            return _products;
        }

        public Product? GetProductByID(int id)
        {
            _product = _productRepository.GetProductByID(id);

            return _product;
        }
        public IEnumerable<Product> CheckDiscount(List<Product> products)
        {
            List<Product> updatedproducts = products;

            Days currentday = (Days)DateTime.Now.DayOfWeek;
            // check for day
            if(currentday == Days.Saturday)
            {
                foreach(Product product in updatedproducts)
                {
                    product.SetPrice(product.Price);
                }
            }
            return updatedproducts;
        }
        private enum Days
        {
            None = 0,
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
            Sunday = 7
        }
    }
}