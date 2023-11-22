using Product.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Interfaces
{
    public interface IProductData
    {
        List<ProductDTO> GetProductDTOs();
    }
}
