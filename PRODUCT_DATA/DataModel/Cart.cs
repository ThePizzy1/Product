using System;
using System.Collections.Generic;
using System.Text;

namespace PRODUCT_DATA.DataModel
{
   public partial class Cart
    {
        public Guid Id { get; set; }        //PK hash id koji bi se izmjenio ako dođe do brisanja baze, automatski se sam generira 
        public string UserId { get; set; }  // korisnik FK
        public string CartId { get; set; } //šifra za povezivanje košarice sa proizvodom, radim to ovako kakao u slučaju da se pobriše baza podatak može se ponovo spojit jel bi pravi id bio drugačiji
       
    }
}
