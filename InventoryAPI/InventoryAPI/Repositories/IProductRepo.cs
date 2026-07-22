using InventoryAPI.Models;

namespace InventoryAPI.Repositories
{
    public interface IProductRepo
    {
        Task<Product> AddProduct(Product Product, long CategoryId);
        Task<Product> UpdateProduct(long ProductId, string ProductName);
        Task<bool> DeleteProduct(long ProductId);
        Task<Product> GetProductById(long ProductId);
        Task<IEnumerable<Product>> GetProducts();
    }
}
