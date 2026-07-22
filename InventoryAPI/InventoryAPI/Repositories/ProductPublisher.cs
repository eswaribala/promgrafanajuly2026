using Confluent.Kafka;
using InventoryAPI.Models;
using System.Diagnostics;
using System.Net;
using static GraphQL.Validation.Rules.OverlappingFieldsCanBeMerged;

namespace InventoryAPI.Repositories
{
    public class ProductPublisher : IProductPublisher
    {
        

        public async Task<string> PublishProduct(string topicName, string message, IConfiguration configuration)
        {
            ProducerConfig ProducerConfig = new ProducerConfig
            {
                BootstrapServers = configuration["BootStrapServer"],
                ClientId = Dns.GetHostName()
            };

            try
            {
                using (var producer = new ProducerBuilder
                <Null, string>(ProducerConfig).Build())
                {
                    var result = await producer.ProduceAsync
                    (topicName, new Message<Null, string>
                    {
                        Value = message
                    });

                    Debug.WriteLine($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");
                    return await Task.FromResult($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult("Not Published.....");
        }
    }
}
