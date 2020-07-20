using jostva.Commerce.Order.Service.Proxies.Catalog.Commands;
using jostva.Commerce.Order.Service.Proxies.Catalog.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace jostva.Commerce.Order.Service.Proxies.Catalog
{
    public class CatalogHttpProxy : ICatalogProxy
    {
        private const string MediaType = "application/json";

        private readonly HttpClient httpClient;
        private readonly ApiUrls apiUrls;


        public CatalogHttpProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls)
        {
            this.httpClient = httpClient;
            this.apiUrls = apiUrls.Value;
        }


        public async Task UpdateStockAsync(ProductInStockUpdateStockCommand command)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(command),
                                                      Encoding.UTF8, MediaType);

            HttpResponseMessage request = await httpClient.PutAsync(apiUrls.CatalogUrl + "api/stocks", content);
            request.EnsureSuccessStatusCode();
        }
    }
}