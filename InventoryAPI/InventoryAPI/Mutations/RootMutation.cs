using GraphQL;
using GraphQL.Types;
using InventoryAPI.Models;
using InventoryAPI.Queries;
using InventoryAPI.Repositories;

namespace InventoryAPI.Mutations
{
    public class RootMutation:ObjectGraphType
    {
        private ICategoryRepo _categoryRepo;
        private IProductRepo _productRepo;

        public RootMutation(ICategoryRepo categoryRepo,IProductRepo productRepo)
        {
            this._categoryRepo = categoryRepo;
            this._productRepo = productRepo;

            Name = "InventoryMutation";

            FieldAsync<CategoryGLType>("insertCategory",
            arguments: new QueryArguments(

                new QueryArgument<CategoryGLInputType> { Name = "category" }),

            resolve: async context =>
            {

                var category = context.GetArgument<Category>("category");

                var result = await InsertCategory(category);
                return result;
            });

            FieldAsync<CategoryGLType>("updateCategory",
            arguments: new QueryArguments(

                new QueryArgument<LongGraphType> { Name = "categoryId" },
                new QueryArgument<StringGraphType> { Name = "categoryName" }

                ),

            resolve: async context =>
            {

                var categoryId = context.GetArgument<long>("categoryId");
                var categoryName = context.GetArgument<string>("categoryName");
                var result = await UpdateCategory(categoryId,categoryName);
                return result;
            });


            FieldAsync<StringGraphType>("deleteCategory",
            arguments: new QueryArguments(

                new QueryArgument<LongGraphType> { Name = "categoryId" }
             

                ),

            resolve: async context =>
            {

                var categoryId = context.GetArgument<long>("categoryId");
             
                await DeleteCategory(categoryId);
                return $"CategoryId {categoryId} is successfully deleted";
            });

            //product


            FieldAsync<ProductGLType>("insertProduct",
            arguments: new QueryArguments(

                new QueryArgument<ProductGLInputType> { Name = "product" },
                new QueryArgument<LongGraphType> { Name = "categoryId" }

                ),

            resolve: async context =>
            {

                var product = context.GetArgument<Product>("product");
                var categoryId = context.GetArgument<long>("categoryId");
                var result = await InsertProduct(categoryId,product);
                return result;
            });

            FieldAsync<ProductGLType>("updateProduct",
            arguments: new QueryArguments(

                new QueryArgument<LongGraphType> { Name = "productId" },
                new QueryArgument<StringGraphType> { Name = "productName" }

                ),

            resolve: async context =>
            {

                var productId = context.GetArgument<long>("productId");
                var productName = context.GetArgument<string>("productName");
                var result = await UpdateProduct(productId, productName);
                return result;
            });


            FieldAsync<StringGraphType>("deleteProduct",
            arguments: new QueryArguments(

                new QueryArgument<LongGraphType> { Name = "productId" }


                ),

            resolve: async context =>
            {

                var productId = context.GetArgument<long>("productId");

                await DeleteProduct(productId);
                return $"ProductId {productId} is successfully deleted";
            });



        }


        private async Task<Category> InsertCategory(Category Category)
        {
            if (Category == null)
                return null;
            else
            {
                return await _categoryRepo.AddCategory(Category);
            }
        }
        private async Task<Category> UpdateCategory(long categoryId, string categoryName)
        {
            if (categoryId <= 0)
                return null;
            else
            {
                return await _categoryRepo.UpdateCategory(categoryId, categoryName);
            }
        }

        private async Task<bool> DeleteCategory(long categoryId)
        {
            if (categoryId <= 0)
                return false;
            else
            {
                return await _categoryRepo.DeleteCategory(categoryId);
            }
        }



        private async Task<Product> InsertProduct(long categoryId, Product product)
        {
            if (categoryId <=0 )
                return null;
            else
            {
                return await _productRepo.AddProduct(product, categoryId);  
            }
        }

        private async Task<Product> UpdateProduct(long productId, string productName)
        {
            if (productId <= 0)
                return null;
            else
            {
                return await _productRepo.UpdateProduct(productId, productName);
            }
        }


        private async Task<bool> DeleteProduct(long productId)
        {
            if (productId <= 0)
                return false;
            else
            {
                return await _productRepo.DeleteProduct(productId);
            }
        }

    }
}
