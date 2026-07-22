using InventoryAPI.Models;
using InventoryAPI.Repositories;
using InventoryService.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    //[Route("api/[controller]")]
   
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
   [EnableCors]
    //Content negotiation
    //[Produces("application/xml")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryRepo categoryRepo;

        public CategoriesController(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }



        [Authorize(Roles = Roles.Admin)]
        // GET: api/<CategoryController>
        [HttpGet]
       // [MapToApiVersion("2.0")]
        public async Task<IEnumerable<Category>> Get()
        {
            return await categoryRepo.GetCategories();
        }

        [HttpGet("{Id}")]
        public async Task<Category> Get(int Id)
        {
            return await categoryRepo.GetCategoryById(Id);
        }
      
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category Category)
        {
            await categoryRepo.AddCategory(Category);
            return CreatedAtAction(nameof(Get),
                         new { id = Category.CategoryId }, Category);

        }


        // PUT api/<CategoryController>/5
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] string CategoryName)
        {
            var result = await categoryRepo.UpdateCategory(Id, CategoryName);
            return CreatedAtAction(nameof(Get),
                         new { id = result.CategoryId }, result);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (await categoryRepo.DeleteCategory(Id))
                return new OkResult();
            else
                return new BadRequestResult();
        }

    }
}
