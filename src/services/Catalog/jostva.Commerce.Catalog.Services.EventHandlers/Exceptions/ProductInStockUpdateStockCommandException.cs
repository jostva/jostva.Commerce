using System;

namespace jostva.Commerce.Catalog.Services.EventHandlers.Exceptions
{
    public class ProductInStockUpdateStockCommandException : Exception
    {
        public ProductInStockUpdateStockCommandException(string message)
            : base(message)
        {

        }
    }
}