using GraphQL.Types;
using InventoryAPI.Models;

namespace InventoryAPI.Queries
{
    public class ProductGLType:ObjectGraphType<Product>
    {
        public ProductGLType()
        {
            Name = "Product";
            Field(_ => _.ProductId).Description("Product Id");
            Field(_ => _.ProductName).Description("Product Name");
            Field(_ => _.SKU).Description("SKU");
            Field(_ => _.productDescription.PurchasedDate).Description("Purchase Date");
            Field(_ => _.productDescription.Cost).Description("Cost");
            Field(_ => _.productDescription.ExpiryDate).Description("Expiry Date");
            Field(_ => _.productDescription.BufferLevel).Description("Buffer Level");
        }
    }
}
