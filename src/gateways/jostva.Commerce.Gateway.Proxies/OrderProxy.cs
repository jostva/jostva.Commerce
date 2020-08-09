#region usings

using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Order.Commands;
using jostva.Commerce.Gateway.Models.Order.DTOs;
using jostva.Commerce.Gateway.Proxies.Config;
using jostva.Commerce.Gateway.Proxies.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Gateway.Proxies
{
    public class OrderProxy : IOrderProxy
    {
        #region attributes

        private readonly ApiUrls apiUrls;
        private readonly HttpClient httpClient;

        #endregion

        #region constructor

        public OrderProxy(HttpClient httpClient,
                          IOptions<ApiUrls> apiUrls,
                          IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            this.httpClient = httpClient;
            this.apiUrls = apiUrls.Value;
        }

        #endregion

        #region methods

        public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take)
        {
            var request = await httpClient.GetAsync($"{apiUrls.OrderUrl}api/orders?page={page}&take={take}");
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
            var request = await httpClient.GetAsync($"{apiUrls.OrderUrl}api/orders/{id}");
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

            var request = await httpClient.PostAsync($"{apiUrls.OrderUrl}api/orders", content);
            request.EnsureSuccessStatusCode();
        }

        #endregion
    }
}