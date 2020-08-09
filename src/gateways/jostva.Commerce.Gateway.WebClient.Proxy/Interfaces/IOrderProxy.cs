using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Order.Commands;
using jostva.Commerce.Gateway.Models.Order.DTOs;
using System.Threading.Tasks;

namespace jostva.Commerce.Gateway.WebClient.Proxy.Interfaces
{
    public interface IOrderProxy
    {
        /// <summary>
        /// Este método no trae la información de los productos.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<DataCollection<OrderDto>> GetAllAsync(int page, int take);

        /// <summary>
        /// Trae la información completa de la orden haciendo cruce con los diferentes microservicios.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OrderDto> GetAsync(int id);

        /// <summary>
        /// Creación de órdenes de compra
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task CreateAsync(OrderCreateCommand command);
    }
}