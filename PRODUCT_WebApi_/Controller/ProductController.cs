using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRODUCT_DOMAIN;
using PRODUCT_LOGIC;
using PRODUCT_LOGIC_I;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRODUCT_WebApi_.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDomain product)
        {
            await _logic.AddProductAsync(product);
            return Ok();
        }

        // --- CART ---
        [HttpPost("cart/{username}/add/{productId}")]
        public async Task<IActionResult> AddToCart(string username, string productId)
        {
            await _logic.AddToCartAsync(username, productId);
            return Ok();
        }

        [HttpPost("cart/{username}/remove/{productId}")]
        public async Task<IActionResult> RemoveFromCart(string username, string productId)
        {
            await _logic.RemoveFromCartAsync(username, productId);
            return Ok();
        }

        [HttpGet("cart/{username}")]
        public async Task<IActionResult> GetCart(string username)
        {
            var cart = await _logic.GetCartAsync(username);
            return Ok(cart);
        }
    }


}
