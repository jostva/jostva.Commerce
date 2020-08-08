#region usings

using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Catalog.DTOs;
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
    public class CatalogProxy : ICatalogProxy
    {
        #region attributes

        private readonly ApiUrls apiUrls;
        private readonly HttpClient httpClient;

        #endregion

        #region constructor

        public CatalogProxy(HttpClient httpClient,
                            IOptions<ApiUrls> apiUrls,
                            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            this.httpClient = httpClient;
            this.apiUrls = apiUrls.Value;
        }

        #endregion

        #region methods

        public async Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var ids = string.Join(',', clients ?? new List<int>());

            var request = await httpClient.GetAsync($"{apiUrls.CatalogUrl}api/products?page={page}&take={take}&ids={ids}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ProductDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }


        public async Task<ProductDto> GetAsync(int id)
        {
            var request = await httpClient.GetAsync($"{apiUrls.CatalogUrl}api/products/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ProductDto>(
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