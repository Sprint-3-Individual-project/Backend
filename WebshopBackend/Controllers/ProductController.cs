using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProduct;
using ShopProduct.Interfaces;
using WebshopBackend.DTOs;
using WebshopBackend.Exceptions;

namespace WebshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [EnableCors("_webshop_frontend")]
    public class ProductController : ControllerBase
    {
        IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                List<ProductDTO> _dtos = new List<ProductDTO>();
                List<Product> products = (List<Product>)_productManager.GetAllProducts();

                foreach(Product product in products)
                {
                    ProductDTO dto = ProductDTO.CastProduct(product); // TODTO function.
                    _dtos.Add(dto);
                }
                return Ok(_dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetProductByID(int id)
        {
            try
            {
                Product product = _productManager.GetProductByID(id);
                ProductDTO _dto = ProductDTO.CastProduct(product);
                return Ok(_dto);
            }
            catch (ProductNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
