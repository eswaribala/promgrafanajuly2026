
using GraphQL;
using GraphQL.Types;
using InventoryAPI.Repositories;

namespace InventoryAPI.Queries
{
    public class CategoryGLQuery:ObjectGraphType
    {
     

            public CategoryGLQuery(ICategoryRepo categoryRepo)
            {
                Name = "CategoryQuery";
                //get all categories
                Field<ListGraphType<CategoryGLType>>(
                  "categories",
                  resolve: context => categoryRepo.GetCategories()
              );

                //get category by id
                Field<CategoryGLType>(
                   "category",
                   arguments: new QueryArguments(new QueryArgument<LongGraphType>
                   { Name = "categoryId" }),
                   resolve: context => categoryRepo.GetCategoryById(context.GetArgument<long>("categoryId"))

                   );

            }

    }
}
