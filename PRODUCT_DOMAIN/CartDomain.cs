using System;
using System.Collections.Generic;
using System.Text;

namespace PRODUCT_DOMAIN
{
  public  class CartDomain
    {
        public CartDomain() { }
        public Guid Id { get; set; }        //PK hash id koji bi se izmjenio ako dođe do brisanja baze, automatski se sam generira 
        public string UserId { get; set; }  // korisnik FK
        public string CartId { get; set; }
    }
}
