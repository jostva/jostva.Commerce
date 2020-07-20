using jostva.Commerce.Order.Service.Proxies.Catalog.Commands;
using jostva.Commerce.Order.Service.Proxies.Catalog.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace jostva.Commerce.Order.Service.Proxies.Catalog
{
    public class CatalogQueueProxy : ICatalogProxy
    {
        private readonly string connectionString;

        public CatalogQueueProxy(IOptions<AzureServiceBus> azure)
        {
            connectionString = azure.Value.ConnectionString;
        }


        public async Task UpdateStockAsync(ProductInStockUpdateStockCommand command)
        {
            var queueClient = new QueueClient(connectionString, "order-stock-update");

            // Serialize message
            string body = JsonSerializer.Serialize(command);
            var message = new Message(Encoding.UTF8.GetBytes(body));

            // Send the message to the queue
            await queueClient.SendAsync(message);

            // Close
            await queueClient.CloseAsync();
        }
    }
}