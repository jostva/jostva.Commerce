using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Catalog.DTOs;
using jostva.Commerce.Gateway.WebClient.Proxy.Config;
using jostva.Commerce.Gateway.WebClient.Proxy.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace jostva.Commerce.Gateway.WebClient.Proxy
{
    public class ProductProxy : IProductProxy
    {
        private readonly string apiGatewayUrl;
        private readonly HttpClient httpClient;


        public ProductProxy(HttpClient httpClient,
                            ApiGatewayUrl apiGatewayUrl,
                            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            this.httpClient = httpClient;
            this.apiGatewayUrl = apiGatewayUrl.Value;
        }


        public async Task<DataCollection<ProductDto>> GetAllAsync(int page, int take)
        {
            var request = await httpClient.GetAsync($"{apiGatewayUrl}api/products?page={page}&take={take}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ProductDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}