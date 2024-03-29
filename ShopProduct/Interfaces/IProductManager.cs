﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProduct.Interfaces
{
    public interface IProductManager
    {
        IEnumerable<Product> GetAllProducts();
        Product? GetProductByID(int id);
        Task AddProduct(Product product);
        void UpdateProductPrice(int id, decimal newPrice);
    }
}
