using GraphQL.Types;
using InventoryAPI.Models;

namespace InventoryAPI.Queries
{
    public class CategoryGLType:ObjectGraphType<Category>
    {
        public CategoryGLType()
        {
            Name = "Category";
            Field(_ => _.CategoryId).Description("Category Id");
            Field(_ => _.CategoryName).Description("Category Name");
        }

    }
}
