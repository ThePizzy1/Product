using System;
using System.Collections.Generic;
using System.Text;

namespace PRODUCT_DOMAIN
{
    public class ProductDomain
    {
        public ProductDomain() { }
        public ProductDomain(string idProduct, string name, string des, decimal price, decimal weight, string image, int wMonts, DateTime expirationDate, string keywords) {
            idProduct = IdProduct;
            name = Name;
            des = Description;
            price = Price;
            weight = (decimal)Weight;
            image = ImageUrl; 
            wMonts = (int)WarrantyMonths;
            expirationDate = (DateTime)ExpirationDate;
            keywords = Keywords;



        }
        public Guid Id { get; set; } // PK hash automatski generiran
        public string IdProduct { get; set; } // šifra proizvoda, upisuje korisnik
        public string Name { get; set; } // naziv proizvoda
        public string Description { get; set; } // opis proizvoda
        public decimal Price { get; set; }
        public decimal? Weight { get; set; } 
        public string ImageUrl { get; set; } // URL slike
        public int? WarrantyMonths { get; set; } // opcionalno
        public DateTime? ExpirationDate { get; set; } // opcionalno
        public string Keywords { get; set; } // filtriranje po ključnim riječima
    }
}
