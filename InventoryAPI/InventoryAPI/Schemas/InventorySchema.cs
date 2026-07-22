using GraphQL.Types;
using InventoryAPI.Mutations;
using InventoryAPI.Queries;

namespace InventoryAPI.Schemas
{
    public class InventorySchema:Schema
    {
        public InventorySchema(IServiceProvider ServiceProvider)
        {
            Query = ServiceProvider.GetRequiredService<RootQuery>();
            Mutation = ServiceProvider.GetRequiredService<RootMutation>();
        }
    }
}
