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

        [HttpGet]
        [Route("singleItem/{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _logic.GetProductAsync(id);
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
        //on kao šalje username, njega je lako dobit u frontu, sam iz tokena se poviuče
        //username je isto tako jedinstven tak da se ne moram bojat da će slučajno biti dva ista 
        //u drugom dijelu pronađe osobu sa tim username i onda uzme njen id, onaj dugačak hash
        //i u tokenu su i usernam i id, tak da je svejedno 
        [HttpPost("cartAdd")]
        public async Task<IActionResult> AddToCart(
            [FromQuery] string username,
            [FromQuery] string productId,
            [FromQuery] int numberOfItems)
        {
            try
            {
                await _logic.AddToCartAsync(username, productId, numberOfItems);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to add to cart: {ex.Message}");
            }
        }


        [HttpPost("removeItem")]
        public async Task<IActionResult> RemoveItem([FromQuery] string username, [FromQuery] string productId)
        {
            try
            {
                await _logic.RemoveFromCartAsync(username, productId);
                return Ok(new { message = "Item removed from cart" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to remove item: {ex.Message}");
            }
        }

      
        [HttpPost("decrementItem")]
        public async Task<IActionResult> DecrementItem(
            [FromQuery] string username,
            [FromQuery] string productId
             )
        {
            try
            {
                await _logic.RemoveFromCartAsyncDecrement(username, productId);
                return Ok(new { message = $"Item quantity decreased by 1" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to decrement item: {ex.Message}");
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
