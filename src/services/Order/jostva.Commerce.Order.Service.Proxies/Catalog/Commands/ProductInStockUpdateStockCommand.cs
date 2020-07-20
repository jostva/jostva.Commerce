using System.Collections.Generic;

namespace jostva.Commerce.Order.Service.Proxies.Catalog.Commands
{
    public class ProductInStockUpdateStockCommand                 
    {
        public IEnumerable<ProductInStockUpdateItem> Items { get; set; } = new List<ProductInStockUpdateItem>();
    }
}