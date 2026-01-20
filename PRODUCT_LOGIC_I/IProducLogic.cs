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
        //ofc radi
        Task<IEnumerable<ProductDomain>> GetAllProductsAsync();

        //Radi dohavća pojedini item ali mi je vratilo cross policy jučer // radi zajebala si rutu
        Task<ProductDomain> GetProductAsync(string productId);

        //Ovo radi ne dirj to
        public Task<ProductDomain> AddProductAsync(string idProduct,
             string name,
             string description,
             decimal price,
             decimal? weight,
             byte[] image,
             int? warrantyMonths,
             DateTime? expirationDate,
             string keywords);

        // Cart
        Task AddToCartAsync(string username, string productId, int numberOfItems);//prvo moraš ovo 
        Task RemoveFromCartAsync(string username, string productId, int numberOfItems);// da bi mogla ovo
        Task<CartDomain> GetCartAsync(string username);//da vidiš ovo
    }
}
