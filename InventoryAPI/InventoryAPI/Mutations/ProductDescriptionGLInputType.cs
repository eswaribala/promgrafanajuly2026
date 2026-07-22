using GraphQL.Types;

namespace InventoryAPI.Mutations
{
    public class ProductDescriptionGLInputType:InputObjectGraphType
    {
        public ProductDescriptionGLInputType()
        {
            Name = "ProductDescriptionInput";
            Field<NonNullGraphType<DateGraphType>>("PurchaseDate");
            Field<NonNullGraphType<DateGraphType>>("ExpiryDate");
            Field<NonNullGraphType<LongGraphType>>("Cost");
            Field<NonNullGraphType<LongGraphType>>("BufferLevel");

        }
    }
}
