using jostva.Commerce.Customer.Service.Queries.DTOs;
using jostva.Infrastructure.Collection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jostva.Commerce.Customer.Service.Queries.Interfaces
{
    public interface IClientQueryService
    {
        Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);

        Task<ClientDto> GetAsync(int id);
    }
}