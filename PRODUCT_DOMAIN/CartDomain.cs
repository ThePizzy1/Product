using PRODUCT_DATA.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRODUCT_DOMAIN
{
  public  class CartDomain
    {
        public CartDomain() { }
        public CartDomain(string userId, string cartId)
        {
            userId = UserId;
            cartId = CartId;
        }
        public Guid Id { get; set; }        //PK hash id koji bi se izmjenio ako dođe do brisanja baze, automatski se sam generira 
        public string UserId { get; set; }  // korisnik FK
        public string CartId { get; set; }
        public List<CartItemDomain> Items { get; set; } = new List<CartItemDomain>();
    }
}
