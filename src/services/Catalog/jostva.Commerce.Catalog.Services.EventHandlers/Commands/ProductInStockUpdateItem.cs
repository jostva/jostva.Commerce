using jostva.Commerce.Catalog.Common;

namespace jostva.Commerce.Catalog.Services.EventHandlers.Commands
{
    public class ProductInStockUpdateItem
    {
        public int ProductId { get; set; }
        
        public int Stock { get; set; }
        
        public ProductInStockAction Action { get; set; }
    }
}