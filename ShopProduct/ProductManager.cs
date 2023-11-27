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

            return _products;
        }

        public Product? GetProductByID(int id)
        {
            _product = _productRepository.GetProductByID(id);

            return _product;
        }
    }
}