using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Contexts
{
    public class InventoryIdentityContext:IdentityDbContext
    {
        public InventoryIdentityContext(DbContextOptions<InventoryIdentityContext> options):base(options) { 
        
          this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
