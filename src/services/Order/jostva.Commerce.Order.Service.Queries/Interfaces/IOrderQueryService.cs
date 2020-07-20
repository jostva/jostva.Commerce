using jostva.Commerce.Order.Service.Queries.DTOs;
using jostva.Infrastructure.Collection;
using System.Threading.Tasks;

namespace jostva.Commerce.Order.Service.Queries.Interfaces
{
    public  interface IOrderQueryService
    {
        Task<DataCollection<OrderDto>> GetAllAsync(int page, int take);
        
        Task<OrderDto> GetAsync(int id);
    }
}