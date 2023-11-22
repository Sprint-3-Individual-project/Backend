using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProduct.Interfaces
{
    public interface IProductData
    {
        List<Product> GetProductDTOs();
    }
}
