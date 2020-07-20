using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace jostva.Commerce.Catalog.Services.EventHandlers.Commands
{
    public class ProductInStockUpdateStockCommand : INotification
    {
        public IEnumerable<ProductInStockUpdateItem> Items { get; set; } = new List<ProductInStockUpdateItem>();
    }
}
