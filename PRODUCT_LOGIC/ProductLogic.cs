using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRODUCT_DATA.DataModel;
using PRODUCT_DOMAIN;
using PRODUCT_DOMAIN.DataModel;
using PRODUCT_LOGIC_I;

namespace PRODUCT_LOGIC
{
    public class ProductLogic : IProducLogic
    {
        private readonly ProductDbContext _db;

        public ProductLogic(ProductDbContext db)
        {
            _db = db;
        }
        private readonly List<ProductDomain> _products = new List<ProductDomain>();
        private readonly List<CartDomain> _carts = new List<CartDomain>();
        private readonly List<CartItemDomain> _cartItems = new List<CartItemDomain>();

        // --- PRODUCTS ---
        public async Task<IEnumerable<ProductDomain>> GetAllProductsAsync()
        {
            return await _db.Products
                .Select(p => new ProductDomain(
                    p.IdProduct, p.Name, p.Description, p.Price, p.Weight, p.Image,
                    p.WarrantyMonths, p.ExpirationDate, p.Keywords))
                .ToListAsync();
        }

        public async Task<ProductDomain> GetProductAsync(string productId)
        {
            var p = await _db.Products.FirstOrDefaultAsync(x => x.IdProduct == productId);
            if (p == null) return null;

            return new ProductDomain(
                p.IdProduct, p.Name, p.Description, p.Price, p.Weight, p.Image,
                p.WarrantyMonths, p.ExpirationDate, p.Keywords
            );
        }

        public async Task<ProductDomain> AddProductAsync(
            string idProduct,
            string name,
            string description,
            decimal price,
            decimal? weight,
            byte[] image,
            int? warrantyMonths,
            DateTime? expirationDate,
            string keywords)
        {
            if (string.IsNullOrWhiteSpace(idProduct))
                throw new ArgumentException("Product ID is required.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.");

            var product = new Product
            {
                IdProduct = idProduct,
                Name = name,
                Description = description,
                Price = price,
                Weight = weight,
                Image = image,
                WarrantyMonths = warrantyMonths,
                ExpirationDate = expirationDate,
                Keywords = keywords
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return new ProductDomain(
                product.IdProduct, product.Name, product.Description, product.Price,
                product.Weight, product.Image, product.WarrantyMonths, product.ExpirationDate, product.Keywords
            );
        }



        // --- CART FUNCTIONS ---

        public async Task AddToCartAsync(string username, string productId, int numberOfItems)
        {
            // 1. Dohvati ili stvori cart
            var cart = await _db.Carts.FirstOrDefaultAsync(c => c.UserId == username);
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = username,
                    CartId = await GenerateUniqueCartIdAsync()

                };
                _db.Carts.Add(cart);
                await _db.SaveChangesAsync();
            }

            // 2. Provjeri postoji li već item u cartu
            var exists = await _db.CartItems
                .AnyAsync(ci => ci.CartId == cart.CartId && ci.ProductId == productId);

            if (!exists)
            {
                var cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.CartId,
                    ProductId = productId
                };
                _db.CartItems.Add(cartItem);
                await _db.SaveChangesAsync();
            }
            if(exists)
            {

            }
            else
            {
                throw new Exception($"Product mistake");
            }
        }
        //generator za dodavanje novog cartId ako već ne postoji
        private static string GenerateRandomCartId(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();

            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[random.Next(chars.Length)])
                .ToArray());
        }
        //neka provjeri dali taj id postoji ako da ponovi radnju dok ne bude jedinstven
        private async Task<string> GenerateUniqueCartIdAsync()
        {
            string cartId;
            bool exists;

            do
            {
                cartId = GenerateRandomCartId(4); 
                exists = await _db.Carts.AnyAsync(c => c.CartId == cartId);
            }
            while (exists);

            return cartId;
        }

        public async Task RemoveFromCartAsync(string username, string productId, int numberOfItems)
        {
            var cart = await _db.Carts.FirstOrDefaultAsync(c => c.UserId == username);
            if (cart == null) return; // nema carta → ništa za ukloniti

            var cartItem = await _db.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == productId);

            if (cartItem != null)
            {
                _db.CartItems.Remove(cartItem);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<CartDomain> GetCartAsync(string username)
        {
         
            var cart = await _db.Carts.FirstOrDefaultAsync(c => c.UserId == username);//koristim username i on je jedinstven a ne da mi se za ovak malo dohvaćati hash

        
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = username,
                    CartId = Guid.NewGuid().ToString()
                };
                _db.Carts.Add(cart);
                await _db.SaveChangesAsync();
            }

           
            var cartItems = await _db.CartItems
                .Where(ci => ci.CartId == cart.CartId)
                .Include(ci => ci.Product) 
                .ToListAsync();

            var cartDomain = new CartDomain
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartId = cart.CartId,
                Items = cartItems.Select(ci => new CartItemDomain
                {
                    Id = ci.Id,
                    CartId = ci.CartId,
                    ProductId = ci.ProductId,
                    ProductD = new ProductDomain(
                        ci.Product.IdProduct,
                        ci.Product.Name,
                        ci.Product.Description,
                        ci.Product.Price,
                        ci.Product.Weight,
                        ci.Product.Image,
                        ci.Product.WarrantyMonths,
                        ci.Product.ExpirationDate,
                        ci.Product.Keywords
                    )
                }).ToList()
            };

            return cartDomain;
        }


    }
}
