#region usings

using jostva.Commerce.Order.Data;
using jostva.Commerce.Order.Domain;
using jostva.Commerce.Order.Service.EventHandlers.Commands;
using jostva.Commerce.Order.Service.Proxies.Catalog.Commands;
using jostva.Commerce.Order.Service.Proxies.Catalog.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Order.Service.EventHandlers
{
    public class OrderCreateEventHandler : INotificationHandler<OrderCreateCommand>
    {
        private readonly ApplicationDbContext context;
        private readonly ICatalogProxy catalogProxy;
        private readonly ILogger<OrderCreateEventHandler> logger;


        public OrderCreateEventHandler(ApplicationDbContext context,
                                       ICatalogProxy catalogProxy,
                                       ILogger<OrderCreateEventHandler> logger)
        {
            this.context = context;
            this.catalogProxy = catalogProxy;
            this.logger = logger;
        }


        public async Task Handle(OrderCreateCommand notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("--- New order creation started");
            var entry = new Domain.Order();

            using (var trx = await context.Database.BeginTransactionAsync())
            {
                // 01. Prepare detail
                logger.LogInformation("--- Preparing detail");
                PrepareDetail(entry, notification);

                // 02. Prepare header
                logger.LogInformation("--- Preparing header");
                PrepareHeader(entry, notification);

                // 03. Create order
                logger.LogInformation("--- Creating order");
                await context.AddAsync(entry);
                await context.SaveChangesAsync();

                logger.LogInformation($"--- Order {entry.OrderId} was created");

                // 04. Update Stocks
                logger.LogInformation("--- Updating stock");

                try
                {
                    await catalogProxy.UpdateStockAsync(new ProductInStockUpdateStockCommand
                    {
                        Items = notification.Items.Select(x => new ProductInStockUpdateItem
                        {
                            Action = ProductInStockAction.Substract,
                            ProductId = x.ProductId,
                            Stock = x.Quantity
                        })
                    });
                }
                catch
                {
                    logger.LogError("Order couldn't be created because some of the products don't have enough stock");
                    throw new Exception();
                }

                // Lógica para actualizar el Stock
                await trx.CommitAsync();
            }

            logger.LogInformation("--- New order creation ended");
        }


        private void PrepareDetail(Domain.Order entry, OrderCreateCommand notification)
        {
            entry.Items = notification.Items.Select(x => new OrderDetail
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.Price,
                Total = x.Price * x.Quantity
            }).ToList();
        }


        private void PrepareHeader(Domain.Order entry, OrderCreateCommand notification)
        {
            // Header information
            entry.Status = Common.OrderStatus.Pending;
            entry.PaymentType = notification.PaymentType;
            entry.ClientId = notification.ClientId;
            entry.CreatedAt = DateTime.UtcNow;

            // Sum
            entry.Total = entry.Items.Sum(x => x.Total);
        }
    }
}