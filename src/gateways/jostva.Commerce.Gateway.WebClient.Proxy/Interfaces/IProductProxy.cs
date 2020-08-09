using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Catalog.DTOs;
using System.Threading.Tasks;

namespace jostva.Commerce.Gateway.WebClient.Proxy.Interfaces
{
    public interface IProductProxy
    {
        Task<DataCollection<ProductDto>> GetAllAsync(int page, int take);
    }
}