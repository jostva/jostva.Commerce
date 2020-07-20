#region usings

using jostva.Commerce.Catalog.Common;
using jostva.Commerce.Catalog.Data;
using jostva.Commerce.Catalog.Domain;
using jostva.Commerce.Catalog.Services.EventHandlers.Commands;
using jostva.Commerce.Catalog.Services.EventHandlers.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Catalog.Services.EventHandlers
{
    public class ProductInStockUpdateStockEventHandler : INotificationHandler<ProductInStockUpdateStockCommand>
    {
        #region attributes

        private readonly ApplicationDBContext context;
        private readonly ILogger<ProductInStockUpdateStockEventHandler> logger;

        #endregion

        #region constructor

        public ProductInStockUpdateStockEventHandler(ApplicationDBContext context,
                                                    ILogger<ProductInStockUpdateStockEventHandler> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        #endregion

        #region methods

        public async Task Handle(ProductInStockUpdateStockCommand notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("--- ProductInStockUpdateStockCommand started");

            IEnumerable<int> products = notification.Items.Select(x => x.ProductId);
            List<ProductInStock> stocks = await context.Stocks.Where(x => products.Contains(x.ProductId)).ToListAsync();

            logger.LogInformation("--- Retrieve products from database");

            foreach (var item in notification.Items)
            {
                ProductInStock entry = stocks.SingleOrDefault(x => x.ProductId == item.ProductId);

                if (item.Action == ProductInStockAction.Substract)
                {
                    if (entry == null || item.Stock > entry.Stock)
                    {
                        logger.LogError($"--- Product {entry.ProductId} - doesn't have enough stock");
                        throw new ProductInStockUpdateStockCommandException($"Product {entry.ProductId} - doesn't have enough stock");
                    }

                    entry.Stock -= item.Stock;
                    logger.LogInformation($"--- Product {entry.ProductId} - its stock was subtracted and its new stock is {entry.Stock}");
                }
                else
                {
                    if (entry == null)
                    {
                        entry = new ProductInStock
                        {
                            ProductId = item.ProductId
                        };

                        await context.AddAsync(entry);
                        logger.LogInformation($"--- New stock record was created for {entry.ProductId} because didn't exists before");
                    }

                    entry.Stock += item.Stock;
                    logger.LogInformation($"--- Add stock to product {entry.ProductId}");
                }
            }

            await context.SaveChangesAsync();
            logger.LogInformation("--- ProductInStockUpdateStockCommand ended");
        }

        #endregion
    }
}