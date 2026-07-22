using InventoryAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InventoryAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductPublisherController : ControllerBase
    {
        private IProductPublisher _productPublisher;
        private IConfiguration _configuration;
        private IProductRepo _productRepo;

        public ProductPublisherController(IProductPublisher productPublisher, IConfiguration configuration, IProductRepo productRepo)
        {
            _productPublisher = productPublisher;
            _configuration = configuration;
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string topicName = this._configuration["TopicName"];
           var products= await _productRepo.GetProducts();
            string message = JsonSerializer.Serialize(products);
            return Ok(await _productPublisher.PublishProduct(topicName, message, _configuration));

        }


    }
}
