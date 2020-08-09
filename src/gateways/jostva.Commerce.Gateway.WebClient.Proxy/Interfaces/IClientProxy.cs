using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Customer.DTOs;
using System.Threading.Tasks;

namespace jostva.Commerce.Gateway.WebClient.Proxy.Interfaces
{
    public interface IClientProxy
    {
        Task<DataCollection<ClientDto>> GetAllAsync(int page, int take);
    }
}