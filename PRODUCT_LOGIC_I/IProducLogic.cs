using PRODUCT_DATA.DataModel;
using PRODUCT_DOMAIN;
using PRODUCT_DOMAIN.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRODUCT_LOGIC_I
{
  public  interface IProducLogic
    {
        // Products
        Task<IEnumerable<ProductDomain>> GetAllProductsAsync();
        Task<ProductDomain> GetProductAsync(string productId);
        Task AddProductAsync(ProductDomain product);

        // Cart
        Task AddToCartAsync(string username, string productId);
        Task RemoveFromCartAsync(string username, string productId);
        Task<CartDomain> GetCartAsync(string username);
    }
}
