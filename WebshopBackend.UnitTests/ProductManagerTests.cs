using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ShopProduct.Interfaces;
using System.Security.Cryptography.X509Certificates;
using ShopProduct;
using DAL;

namespace WebshopBackend.UnitTests
{
    public class ProductManagerTests
    {
        //ik heb de mockproductlist toch gehouden, dit omdat ik de huidige data wil hebben en als ik hiervan een private list van maakte.
        // en deze mee gaf aan mijn mock dan veranderde hij alles in die lijst en kon ik het dus op het einde niet met elkaar vergelijken.
        public List<Product> MockProductList()  // ipv mock TestData benoemen
        {
            List<Product> products = new List<Product>
            {
                new Product(1, "avocado", 3, 4, "https://www.bijdikkie.nl/wp-content/uploads/sites/103/2023/08/Avocados-3d84a3a.jpg"),
                new Product(4, "strawberry", 3, 5, "https://th-thumbnailer.cdn-si-edu.com/Cs6bYXJ_UrqcMVTSF9rK7uhQXSU=/1072x720/filters:no_upscale()/https://tf-cmsv2-smithsonianmag-media.s3.amazonaws.com/filer/39/3c/393c51d9-ce11-49ce-9d41-5ef599dfabea/bn8e34.jpg")
            };
            return products;
        }
        public Product MockProduct()
        {
            Product product = new Product(1, "avocado", 3, 4, "https://www.bijdikkie.nl/wp-content/uploads/sites/103/2023/08/Avocados-3d84a3a.jpg");
            return product;
        }

        [Test]
        public void GetAllProducts_CollectProducts_GivesExpectedListOfProducts()
        {
            // Arrange
            Mock<IProductRepository> productrepository = new Mock<IProductRepository>();
            productrepository.Setup(x => x.GetAllProducts()).Returns(MockProductList());
            List<Product> products = new List<Product>();
            ProductManager productManager = new ProductManager(productrepository.Object);

            // Act
            products = (List<Product>)productManager.GetAllProducts();

            // Assert
            Assert.AreEqual(2, products.Count);
        }

        [Test]
        public void GetProductById_CollectSelectedProduct_GivesExpectedProduct()
        {
            // Arrange
            Mock<IProductRepository> productrepository = new Mock<IProductRepository>();
            productrepository.Setup(x => x.GetProductByID(1)).Returns(MockProduct());
            int GivenId = 1;

            ProductManager productManager = new ProductManager(productrepository.Object);

            // Act
            Product product = productManager.GetProductByID(GivenId);

            // Assert
            Assert.AreEqual("avocado", product.Name);
        }

        [Test] //ochtend
        public void DetermineDiscountMultiplier_CollectDiscountMultiplier_GivesProductWithMorningDiscount()
        {
            // Arrange
            Mock<IProductRepository> productrepository = new Mock<IProductRepository>();
            List<Product> mockProducts = MockProductList();
            productrepository.Setup(x => x.GetAllProducts()).Returns(MockProductList());
            var productManager = new ProductManager(productrepository.Object);
            Clock.SetStaticTime(DateTime.Today.AddHours(11));

            // Act
            int discountPercentage = DetermineDiscountMultiplier(Clock.CurrentTime);
            List<Product> products = productManager.GetAllProducts().ToList();

            // Assert
            
            for (int i = 0; i < mockProducts.Count; i++)
            {
                Assert.AreEqual(mockProducts[i].Price * ((100 - (decimal)discountPercentage) / 100), products[i].Price);
            }
        }

        [Test] //middag
        public void DetermineDiscountMultiplier_CollectDiscountMultiplier_GivesProductWithNoDiscount()
        {
            // Arrange
            Mock<IProductRepository> productrepository = new Mock<IProductRepository>();
            productrepository.Setup(x => x.GetAllProducts()).Returns(MockProductList());
            List<Product> mockProducts = MockProductList();
            var productManager = new ProductManager(productrepository.Object);
            Clock.SetStaticTime(DateTime.Today.AddHours(15));

            // Act
            int discountPercentage = DetermineDiscountMultiplier(Clock.CurrentTime);
            List<Product> products = productManager.GetAllProducts().ToList();

            // Assert
            for (int i = 0; i < mockProducts.Count; i++)
            {
                Assert.AreEqual(mockProducts[i].Price * ((100 - (decimal)discountPercentage) / 100), products[i].Price);
            }
        }

        [Test] //avond
        public void DetermineDiscountMultiplier_CollectDiscountMultiplier_GivesProductWithEveningDiscount()
        {
            // Arrange
            Mock<IProductRepository> productrepository = new Mock<IProductRepository>();
            productrepository.Setup(x => x.GetAllProducts()).Returns(MockProductList());
            List<Product> mockProducts = MockProductList();
            var productManager = new ProductManager(productrepository.Object);
            Clock.SetStaticTime(DateTime.Today.AddHours(22));

            // Act
            int discountPercentage = DetermineDiscountMultiplier(Clock.CurrentTime);
            List<Product> products = productManager.GetAllProducts().ToList();
            // Assert
            for (int i = 0; i < mockProducts.Count; i++)
            {
                Assert.AreEqual(mockProducts[i].Price * ((100 - (decimal)discountPercentage) / 100), products[i].Price);
            }
        }
        private int DetermineDiscountMultiplier(DateTime dateTime)
        {
            var discountManager = new DiscountManager();
            return discountManager.DetermineDiscountMultiplier(dateTime);
        }
    }
}
