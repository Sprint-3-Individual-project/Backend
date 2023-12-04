using ShopProduct.Interfaces;
using System.Runtime.CompilerServices;

namespace ShopProduct
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository _productRepository;
        private List<Product> _products;
        private Product _product;
        private DiscountManager discountManager;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            discountManager = new DiscountManager();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            _products = (List<Product>)_productRepository.GetAllProducts();  // Tijd logica zetten

            // in de property Price Discount berekenen idempotence zodat als ik het meerdere keren uitvoer het resultaat hetzelfde blijft.

            ApplyDiscounts(_products);

            return _products;
        }

        private void ApplyDiscounts(IEnumerable<Product> _products)
        {
            foreach(Product p in _products)
            {
                p.SetDiscountMultiplier(discountManager.DetermineDiscountMultiplier(Clock.CurrentTime));
            }
        }

        public Product? GetProductByID(int id)
        {
            _product = _productRepository.GetProductByID(id);
            _product.SetDiscountMultiplier(discountManager.DetermineDiscountMultiplier(Clock.CurrentTime));

            return _product;
        }
    }
}