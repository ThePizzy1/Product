using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PRODUCT_DOMAIN.DataModel
{
   public partial class  Product
    {
        public Guid Id { get; set; } // PK hash automatski generirana šifra
        public string IdProduct { get; set; }         // šifra proizvoda (min 4 znamenke), upisuje ju korisnik u slučaju brisanja isti proizvod se ponovo može unjeti bez brige da se neće moći povezati sa ostalim tablicama
        public string Name { get; set; }        //svaki proizvod mora imati ime opis, neku cijenu, pretpostavljam težinu jel je to bitno za slanje pošiljke, i sliku
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Weight { get; set; }

        [Column(TypeName = "varbinary(max)")]//bitno za sliku u bazi da zna šta je točno
        public byte[] Image { get; set; }        
        public int? WarrantyMonths { get; set; }    // zavisi o čemu se radi ali večina ima neku garanciju
        public DateTime? ExpirationDate { get; set; }   
        public string Keywords { get; set; }    //dobro bi došlo za neko filtriranje po ključnim riječima
    }
}
