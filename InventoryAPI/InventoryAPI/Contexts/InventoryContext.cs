using InventoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Contexts
{
    public class InventoryContext:DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> dbContextOptions) : base(dbContextOptions)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
