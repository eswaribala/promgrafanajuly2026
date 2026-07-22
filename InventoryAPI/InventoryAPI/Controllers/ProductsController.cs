using GraphQL.Types;
using GraphQLParser;
using InventoryAPI.DTOS;
using InventoryAPI.Models;
using InventoryAPI.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Splunk.Logging;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;
using static Confluent.Kafka.ConfigPropertyNames;

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
    public class ProductsController : ControllerBase
    {
        private IProductRepo productRepo;
        private readonly ILogger<ProductsController> _logger;
        private HttpClientHandler clientHandler;
        public ProductsController(IProductRepo productRepo,ILogger<ProductsController> logger)
        {
            this.productRepo = productRepo;
            this._logger = logger;
            clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
        }


        // GET: api/<CategoryController>
        [HttpGet]
      //  [MapToApiVersion("2.0")]
        public async Task<IEnumerable<Product>> Get()
        {
            //method1
            var httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Authorization = new 
                AuthenticationHeaderValue("Splunk", "413f75e1-3f11-4be4-bc93-2e8fce0e5a7b");
            //data should send via ‘event’ object
            var data = JsonConvert.SerializeObject(new { @event = "Product Info" });
            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync("http://localhost:8088/services/collector",
                stringContent);
            _logger.LogInformation(response.Result.ToString());

            //method2
            string json = @"{
                'event': 'param@example.com',
                
            "; var middleware = new HttpEventCollectorResendMiddleware(100);
            var ecSender = new HttpEventCollectorSender(new Uri("http://localhost:8088"),  //http port as set in global setting
                "413f75e1-3f11-4be4-bc93-2e8fce0e5a7b",  // token
                null,
                HttpEventCollectorSender.SendMode.Sequential,
                0,
                0,
                0,
                middleware.Plugin
            );
           //ecSender.FlushAsync();

            //method 3
            this._logger.LogInformation(stringContent.ToString());
            this._logger.LogInformation(JsonConvert.SerializeObject(new { @event = "Accessing Products" + DateTime.Now }));
            var products = await productRepo.GetProducts();
            foreach(var product in products)
            {
                this._logger.LogInformation(JsonConvert.SerializeObject(new { @event = product.ProductName }));
                this._logger.LogInformation(JsonConvert.SerializeObject(new { @event = product }));
                ecSender.OnError += o => Console.WriteLine(o.Message);
                ecSender.Send(Guid.NewGuid().ToString(), "INFO", null, new { @event = product });


            }

            return products;
        }

        [HttpGet("{Id}")]
        public async Task<Product> Get(int Id)
        {
            return await productRepo.GetProductById(Id);
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> Post(long Id,[FromBody] Product Product)
        {
            await productRepo.AddProduct(Product,Id);
            return CreatedAtAction(nameof(Get),
                         new { id = Product.ProductId }, Product);

        }


        // PUT api/<CategoryController>/5
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] string ProductName)
        {
            var result = await productRepo.UpdateProduct(Id, ProductName);
            return CreatedAtAction(nameof(Get),
                         new { id = result.ProductId }, result);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (await productRepo.DeleteProduct(Id))
                return new OkResult();
            else
                return new BadRequestResult();
        }
    }
}
