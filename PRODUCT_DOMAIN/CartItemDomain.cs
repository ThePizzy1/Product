using PRODUCT_DATA.DataModel;
using PRODUCT_DOMAIN.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRODUCT_DOMAIN
{
   public class CartItemDomain
    {public CartItemDomain() { }
        public CartItemDomain(string cartId, string productId)
        {
            cartId = CartId;
            productId = ProductId;
        }
        public Guid Id { get; set; }        // hash, generira sam 
        public string CartId { get; set; }    // FK na Cart spaja se na onu šifru u cart
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public ProductDomain ProductD { get; set; }
        public string ProductId { get; set; }  // šifra proizvoda FK na proizvod
    }
}
