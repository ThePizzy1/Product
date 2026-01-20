using System;
using System.Collections.Generic;
using System.Text;

namespace PRODUCT_DOMAIN
{
    public class ProductDomain
    {
        public ProductDomain() { }
        public ProductDomain(
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
            IdProduct = idProduct;
            Name = name;
            Description = description;
            Price = price;
            Weight = weight;
            Image = image;
            WarrantyMonths = warrantyMonths;
            ExpirationDate = expirationDate;
            Keywords = keywords;
        }

        public Guid Id { get; set; } // PK hash automatski generiran
        public string IdProduct { get; set; } // šifra proizvoda, upisuje korisnik
        public string Name { get; set; } // naziv proizvoda
        public string Description { get; set; } // opis proizvoda
        public decimal Price { get; set; }
        public decimal? Weight { get; set; } 
        public byte[] Image { get; set; } // bitno byte za sliku 
        public int? WarrantyMonths { get; set; } // opcionalno
        public DateTime? ExpirationDate { get; set; } // opcionalno
        public string Keywords { get; set; } // filtriranje po ključnim riječima
    }
}
