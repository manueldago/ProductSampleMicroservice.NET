using Microsoft.EntityFrameworkCore;
using ProductRegistrationSystemAPI.data.entities;

namespace ProductRegistrationSystemAPI.data.context
{
    public class ProductContext : DbContext
    {
        
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
        
        public DbSet<Product> Product { get; set; }

    }
}
