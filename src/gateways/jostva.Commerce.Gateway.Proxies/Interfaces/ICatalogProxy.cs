using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Catalog.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jostva.Commerce.Gateway.Proxies.Interfaces
{
    public interface ICatalogProxy
    {
        Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);

        Task<ProductDto> GetAsync(int id);
    }
}