using GraphQL;
using GraphQL.Types;
using InventoryAPI.Repositories;

namespace InventoryAPI.Queries
{
    public class RootQuery:ObjectGraphType
    {
        public RootQuery(ICategoryRepo categoryRepo, IProductRepo productRepo)
        {

            Name = "EcommerceQuery";
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



            //get all products
            Field<ListGraphType<ProductGLType>>(
              "products",
              resolve: context => productRepo.GetProducts()
          );

            //get product by id
            Field<ProductGLType>(
               "product",
               arguments: new QueryArguments(new QueryArgument<LongGraphType>
               { Name = "productId" }),
               resolve: context => productRepo.GetProductById(context.GetArgument<long>("productId"))

               );
        }
    }
}
