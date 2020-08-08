using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Customer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jostva.Commerce.Gateway.Proxies.Interfaces
{
    public interface ICustomerProxy
    {
        Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        
        Task<ClientDto> GetAsync(int id);
    }
}