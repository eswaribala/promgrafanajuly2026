using InventoryAPI.Models;

namespace InventoryAPI.Repositories
{
    public interface IProductPublisher
    {
        Task<string> PublishProduct(string topicName, string message, IConfiguration configuration);

    }
}
