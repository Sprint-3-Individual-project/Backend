﻿using DAL.Entities;
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
        public List<Product> MockProductList()
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
        public void GetAllProducts_CollectProducts_GivesUnexpectedListOfProducts()
        {
            // Arrange
            Mock<IProductRepository> productrepository = new Mock<IProductRepository>();
            productrepository.Setup(x => x.GetAllProducts()).Returns(MockProductList());
            List<Product> products = new List<Product>();
            ProductManager productManager = new ProductManager(productrepository.Object);

            // Act
            products = (List<Product>)productManager.GetAllProducts();

            // Assert
            Assert.AreNotEqual(3, products.Count);
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

        [Test]
        public void GetProductById_CollectSelectedProduct_GivesUnexpectedProduct()
        {
            // Arrange
            Mock<IProductRepository> productrepository = new Mock<IProductRepository>();
            productrepository.Setup(x => x.GetProductByID(1)).Returns(MockProduct());
            int GivenId = 1;

            ProductManager productManager = new ProductManager(productrepository.Object);

            // Act
            Product product = productManager.GetProductByID(GivenId);

            // Assert
            Assert.AreNotEqual("SteelDrum", product.Name);
        }
    }
}
