using jostva.Commerce.Order.Service.Proxies.Catalog.Commands;
using System.Threading.Tasks;

namespace jostva.Commerce.Order.Service.Proxies.Catalog.Interfaces
{
    public interface ICatalogProxy
    {
        Task UpdateStockAsync(ProductInStockUpdateStockCommand command);
    }
}