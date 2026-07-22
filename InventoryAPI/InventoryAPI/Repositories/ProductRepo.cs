using InventoryAPI.Contexts;
using InventoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private InventoryContext _context;

        public ProductRepo(InventoryContext context) { 
         _context = context;
        }
        public async Task<Product> AddProduct(Product Product, long CategoryId)
        {
            Category? result = await this._context.Categories.FirstOrDefaultAsync(c =>
         c.CategoryId == CategoryId);

            if (result != null)
            {
                Product.Category = result;
                var productResult = await this._context.Products.AddAsync(Product);
                await this._context.SaveChangesAsync();
                return productResult.Entity;
            }
            else
                return null;
            


        }

        public async Task<bool> DeleteProduct(long ProductId)
        {
            var result = await this._context.Products.FirstOrDefaultAsync(p =>
           p.ProductId == ProductId);


            if (result != null)
            {
                this._context.Products.Remove(result);
                await this._context.SaveChangesAsync();
            }

            result = await this._context.Products.FirstOrDefaultAsync(p =>
            p.ProductId == ProductId);
            if (result == null)
                return true;
            else
                return false;
        }

        public async Task<Product> GetProductById(long ProductId)
        {
            var result = await this._context.Products.FirstOrDefaultAsync(p =>
           p.ProductId == ProductId);
            if (result != null)
                return result;
            else
                return null;


        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await this._context.Products.ToListAsync();
        }

        public async Task<Product> UpdateProduct(long ProductId, string ProductName)
        {
            var result = await this._context.Products.FirstOrDefaultAsync(p =>
         p.ProductId == ProductId);
            if (result != null)
            {
                result.ProductName = ProductName;
                await this._context.SaveChangesAsync();
                return result;

            }
            else
                return null;
        }
    }
}
