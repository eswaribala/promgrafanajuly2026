using InventoryAPI.Models;

namespace InventoryAPI.Repositories
{
    public interface ICategoryRepo
    {
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(long CategoryId, string CategoryName);   
        Task<bool> DeleteCategory(long CategoryId);  
        Task<Category> GetCategoryById(long CategoryId);
        Task<IEnumerable<Category>> GetCategories();
            
    }
}
