using System;
using System.Collections.Generic;
using System.Text;
using PRODUCT_DOMAIN.DataModel;

namespace PRODUCT_DATA.DataModel
{
   public partial class CartItem
    {
        public Guid Id { get; set; }        // hash, generira sam 
        public string CartId { get; set; }    // FK na Cart spaja se na onu šifru u cart
        public string ProductId { get; set; }  // šifra proizvoda FK na proizvod
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
