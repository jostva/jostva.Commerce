using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Order.Commands;
using jostva.Commerce.Gateway.Models.Order.DTOs;
using System.Threading.Tasks;

namespace jostva.Commerce.Gateway.Proxies.Interfaces
{
    public interface IOrderProxy
    {
        Task<DataCollection<OrderDto>> GetAllAsync(int page, int take);
        
        Task<OrderDto> GetAsync(int id);
        
        Task CreateAsync(OrderCreateCommand command);
    }
}