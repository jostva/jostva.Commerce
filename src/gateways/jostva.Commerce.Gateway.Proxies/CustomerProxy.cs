#region usings

using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Customer.DTOs;
using jostva.Commerce.Gateway.Proxies.Config;
using jostva.Commerce.Gateway.Proxies.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Gateway.Proxies
{
    public class CustomerProxy : ICustomerProxy
    {
        #region attributes

        private readonly ApiUrls apiUrls;
        private readonly HttpClient httpClient;

        #endregion

        #region constructor

        public CustomerProxy(HttpClient httpClient,
                             IOptions<ApiUrls> apiUrls,
                             IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            this.httpClient = httpClient;
            this.apiUrls = apiUrls.Value;
        }

        #endregion

        #region methods

        public async Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var ids = string.Join(',', clients ?? new List<int>());

            var request = await httpClient.GetAsync($"{apiUrls.CustomerUrl}api/clients?page={page}&take={take}&ids={ids}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ClientDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }


        public async Task<ClientDto> GetAsync(int id)
        {
            var request = await httpClient.GetAsync($"{apiUrls.CustomerUrl}api/clients/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ClientDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        #endregion
    }
}