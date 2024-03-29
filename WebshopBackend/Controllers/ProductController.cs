﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.JsonWebTokens;
using ShopProduct;
using ShopProduct.Exceptions;
using ShopProduct.Interfaces;
using System.Security.Claims;
using User;
using WebshopBackend.DTOs;
using WebshopBackend.Exceptions;
using WebshopBackend.Hubs;

namespace WebshopBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    [EnableCors("_webshop_frontend")]
    public class ProductController : ControllerBase
    {
        IProductManager _productManager;
        IHubContext<DefaultHub> _hubContext;

        public ProductController(IProductManager productManager, IHubContext<DefaultHub> hubContext)
        {
            _productManager = productManager;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                List<ProductDTO> _dtos = new List<ProductDTO>();
                List<Product> products = (List<Product>)_productManager.GetAllProducts();

                foreach (Product product in products)
                {
                    ProductDTO dto = ProductDTO.CastProduct(product); // TODTO function.
                    _dtos.Add(dto);
                }
                return Ok(_dtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new
                {
                    Message = "Internal Server Error: " + ex.Message
                });
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

        ////voorbeeld
        //[HttpGet("edit")]
        //[Authorize]
        //public IActionResult EditProduct(int id)
        //{
        //    var roleClaim = User.FindFirstValue("role");
        //    if (roleClaim != Role.Admin.ToString())
        //        return Unauthorized();
        //    return Ok();
        //}

        [HttpPut("updatePrice/{id}")]
        public IActionResult UpdateProductById(int id, [FromBody] UpdatePriceDTO updatePriceDTO)
        {
            //var roleClaim = User.FindFirstValue("role");
            //if (roleClaim != Role.Admin.ToString())
            //{
            //    return Unauthorized("You are not authorized to do this");
            //}
            double newPrice = updatePriceDTO.NewPrice;
            Console.WriteLine($"Received request for product {id} with new price: {updatePriceDTO.NewPrice}");

            try
            {

                _productManager.UpdateProductPrice(id, (decimal)newPrice);

                var message = $"Product heeft succesvol de prijs gewijzigd naar {newPrice}";
                _hubContext.Clients.All.SendCoreAsync("BroadCastMessage", new object[] { message });
                return Ok("Product price updated successfully");
                // connectie leggen in de frontend met de websocket
                // als de updateprice succesvol is, stuur een notificatie naar alle clients (daarvoor gebruik ik de hub)
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new
                {
                    Message = "Internal Server Error: " + ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDTO productDto)
        {
            //var roleClaim = User.FindFirstValue("role");
            //if (roleClaim != Role.Admin.ToString())
            //{
            //    return Unauthorized("You are not authorized to do this");
            //}
            try
            {

                _productManager.AddProduct(new Product(0, productDto.name, productDto.price, productDto.stock, productDto.fotourl));

                return Ok("Product added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new
                {
                    Message = "Internal Server Error: " + ex.Message
                });
            }
        }
    }
}
