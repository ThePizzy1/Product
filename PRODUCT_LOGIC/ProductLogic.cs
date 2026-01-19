using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRODUCT_DATA.DataModel;
using PRODUCT_DOMAIN;
using PRODUCT_DOMAIN.DataModel;
using PRODUCT_LOGIC_I;

namespace PRODUCT_LOGIC
{
    public class ProductLogic : IProducLogic
    {
        // in-memory "database"
        private readonly List<ProductDomain> _products = new List<ProductDomain>();
        private readonly List<CartDomain> _carts = new List<CartDomain>();
        private readonly List<CartItemDomain> _cartItems = new List<CartItemDomain>();

        // --- PRODUCTS ---
        public Task<IEnumerable<ProductDomain>> GetAllProductsAsync()
        {
            return Task.FromResult(_products.AsEnumerable());
        }

        public Task<ProductDomain> GetProductAsync(string productId)
        {
            var product = _products.FirstOrDefault(p => p.IdProduct == productId);
            return Task.FromResult(product);
        }

        public Task AddProductAsync(ProductDomain product)
        {
            if (!_products.Any(p => p.IdProduct == product.IdProduct))
                _products.Add(product);

            return Task.CompletedTask;
        }

        // --- CART ---
        public Task AddToCartAsync(string username, string productId)
        {
            // nađi cart korisnika
            var cart = _carts.FirstOrDefault(c => c.UserId == username);
            if (cart == null)
            {
                cart = new CartDomain
                {
                    Id = Guid.NewGuid(),
                    UserId = username,
                    CartId = Guid.NewGuid().ToString()
                };
                _carts.Add(cart);
            }

            // provjeri postoji li već proizvod u cart-u
            if (!_cartItems.Any(ci => ci.CartId == cart.CartId && ci.ProductId == productId))
            {
                var cartItem = new CartItemDomain
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.CartId,
                    ProductId = productId
                };
                _cartItems.Add(cartItem);
            }

            return Task.CompletedTask;
        }

        public Task RemoveFromCartAsync(string username, string productId)
        {
            var cart = _carts.FirstOrDefault(c => c.UserId == username);
            if (cart != null)
            {
                var item = _cartItems.FirstOrDefault(ci => ci.CartId == cart.CartId && ci.ProductId == productId);
                if (item != null)
                    _cartItems.Remove(item);
            }

            return Task.CompletedTask;
        }

        public Task<CartDomain> GetCartAsync(string username)
        {
            var cart = _carts.FirstOrDefault(c => c.UserId == username);
            if (cart == null)
            {
                cart = new CartDomain
                {
                    Id = Guid.NewGuid(),
                    UserId = username,
                    CartId = Guid.NewGuid().ToString()
                };
                _carts.Add(cart);
            }

            // napuni Items listu filtriranjem cartItems po CartId
            cart.Items = _cartItems.Where(ci => ci.CartId == cart.CartId).ToList();

            return Task.FromResult(cart);
        }

    }
}
