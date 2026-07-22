using GraphQL.Types;

namespace InventoryAPI.Mutations
{
    public class CategoryGLInputType:InputObjectGraphType
    {
        public CategoryGLInputType() {

            Name = "CategoryInput";
            //Auto Generated
            //Field<NonNullGraphType<LongGraphType>>("CategoryId");
            Field<NonNullGraphType<StringGraphType>>("CategoryName");
        
        }
    }
}
