using InventoryAPI.Contexts;
using InventoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private InventoryContext _context;

        public CategoryRepo(InventoryContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategory(Category category)
        {
            var result = await this._context.Categories.AddAsync(category);

            await this._context.SaveChangesAsync();

            return result.Entity;

        }

        public async Task<bool> DeleteCategory(long CategoryId)
        {
           var result=  await this._context.Categories.FirstOrDefaultAsync(c =>
            c.CategoryId == CategoryId);
            if(result != null)
            {
                this._context.Categories.Remove(result);
                await  this._context.SaveChangesAsync();
            }

           result = await this._context.Categories.FirstOrDefaultAsync(c =>
           c.CategoryId == CategoryId);
            if (result == null)
                return true;
            else
                return false;

        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await this._context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(long CategoryId)
        {
            var result = await this._context.Categories.FirstOrDefaultAsync(c =>
           c.CategoryId == CategoryId);
            if (result != null)
                return result;
            else
                return null;
        }

        public async Task<Category> UpdateCategory(long CategoryId, string CategoryName)
        {
            var result = await this._context.Categories.FirstOrDefaultAsync(c =>
           c.CategoryId == CategoryId);
            if (result != null)
            {
                result.CategoryName = CategoryName;
                await this._context.SaveChangesAsync();
                return result;

            }
            else
                return null;

        }
    }
}
