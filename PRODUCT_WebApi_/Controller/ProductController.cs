using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRODUCT_DOMAIN;
using PRODUCT_LOGIC;
using PRODUCT_LOGIC_I;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PRODUCT_WebApi_.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ProductController : ControllerBase
    {
        private readonly IProducLogic _logic;

        public ProductController(IProducLogic logic)
        {
            _logic = logic;
        }

        // --- PRODUCTS ---
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _logic.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(string productId)
        {
            var product = await _logic.GetProductAsync(productId);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProductAsync(
           [FromForm] string idProduct,
           [FromForm] string name,
           [FromForm] string description,
           [FromForm] decimal price,
           [FromForm] decimal? weight,
           [FromForm] int? warrantyMonths,
           [FromForm] DateTime? expirationDate,
           [FromForm] string keywords,
           [FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("Image is required.");

            byte[] imageBytes;
            using (var ms = new MemoryStream())
            {
                await image.CopyToAsync(ms);
                imageBytes = ms.ToArray();
            }

            try
            {
                var product = await _logic.AddProductAsync(
                    idProduct, name, description, price, weight, imageBytes,
                    warrantyMonths, expirationDate, keywords
                );

                return Ok(product);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Failed to add product: {ex.Message}");
            }
        }

        //---Cart-----
        [HttpPost("cart/{username}/add/{productId}")]
        public async Task<IActionResult> AddToCart(string username, string productId)
        {
            try
            {
                await _logic.AddToCartAsync(username, productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to add to cart: {ex.Message}");
            }
        }

        [HttpPost("cart/{username}/remove/{productId}")]
        public async Task<IActionResult> RemoveFromCart(string username, string productId)
        {
            try
            {
                await _logic.RemoveFromCartAsync(username, productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to remove from cart: {ex.Message}");
            }
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetCart(string username)
        {
            try
            {
                var cart = await _logic.GetCartAsync(username);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to fetch cart: {ex.Message}");
            }
        }
    }

}
