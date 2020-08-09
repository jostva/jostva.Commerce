using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Order.Commands;
using jostva.Commerce.Gateway.Models.Order.DTOs;
using jostva.Commerce.Gateway.WebClient.Proxy.Config;
using jostva.Commerce.Gateway.WebClient.Proxy.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace jostva.Commerce.Gateway.WebClient.Proxy
{
    public class OrderProxy : IOrderProxy
    {
        private readonly string apiGatewayUrl;
        private readonly HttpClient httpClient;


        public OrderProxy(HttpClient httpClient,
                          ApiGatewayUrl apiGatewayUrl,
                          IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            this.httpClient = httpClient;
            this.apiGatewayUrl = apiGatewayUrl.Value;
        }


        public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take)
        {
            var request = await httpClient.GetAsync($"{apiGatewayUrl}api/orders?page={page}&take={take}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<OrderDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }


        public async Task<OrderDto> GetAsync(int id)
        {
            var request = await httpClient.GetAsync($"{apiGatewayUrl}api/orders/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<OrderDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }


        public async Task CreateAsync(OrderCreateCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );

            var request = await httpClient.PostAsync($"{apiGatewayUrl}api/orders", content);
            request.EnsureSuccessStatusCode();
        }
    }
}