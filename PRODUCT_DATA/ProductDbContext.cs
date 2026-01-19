using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PRODUCT_DATA.DataModel;

namespace PRODUCT_DATA
{
    public class ProductDbContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
    {
        public ProductDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Product;Trusted_Connection=True;");

            return new ProductDbContext(optionsBuilder.Options);
        }
    }
}
