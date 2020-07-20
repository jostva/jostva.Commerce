#region usings

using jostva.Commerce.Catalog.Data;
using jostva.Commerce.Catalog.Domain;
using jostva.Commerce.Catalog.Services.EventHandlers;
using jostva.Commerce.Catalog.Services.EventHandlers.Commands;
using jostva.Commerce.Catalog.Services.EventHandlers.Exceptions;
using jostva.Commerce.Catalog.Tests.Config;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#endregion

namespace jostva.Commerce.Catalog.Tests
{
    [TestClass]
    public class ProductInStockUpdateStockEventHandlerTest
    {
        ILogger<ProductInStockUpdateStockEventHandler> GetLogger
        {
            get
            {
                return new Mock<ILogger<ProductInStockUpdateStockEventHandler>>().Object;
            }
        }


        [TestMethod]
        public void TryToSubstractStockWhenProductsHasStock()
        {
            ApplicationDBContext context = ApplicationDbContextInMemory.Get();

            int productStockId = 1;
            int productId = 1;

            context.Stocks.Add(new ProductInStock
            {
                ProductInStockId = productStockId,
                ProductId = productId,
                Stock = 1
            });

            context.SaveChanges();

            ProductInStockUpdateStockEventHandler handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem
                    {
                        ProductId = productId,
                        Stock = 1,
                        Action = Common.ProductInStockAction.Substract
                    }
                }
            }, new CancellationToken()).Wait();
        }


        [TestMethod]
        [ExpectedException(typeof(ProductInStockUpdateStockCommandException))]
        public void TryToSubstractStockWhenProductsHasntStock()
        {
            ApplicationDBContext context = ApplicationDbContextInMemory.Get();

            int productStockId = 2;
            int productId = 2;

            context.Stocks.Add(new ProductInStock
            {
                ProductInStockId = productStockId,
                ProductId = productId,
                Stock = 1
            });

            context.SaveChanges();

            ProductInStockUpdateStockEventHandler handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

            try
            {
                handler.Handle(new ProductInStockUpdateStockCommand
                {
                    Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem
                    {
                        ProductId = productId,
                        Stock = 2,
                        Action = Common.ProductInStockAction.Substract
                    }
                }
                }, new CancellationToken()).Wait();
            }
            catch (AggregateException ae)
            {
                if (ae.GetBaseException() is ProductInStockUpdateStockCommandException)
                {
                    throw new ProductInStockUpdateStockCommandException(ae.InnerException?.Message);
                }
            }
        }


        [TestMethod]
        public void TryToAddStockWhenProductExists()
        {
            ApplicationDBContext context = ApplicationDbContextInMemory.Get();

            int productStockId = 3;
            int productId = 3;

            context.Stocks.Add(new ProductInStock
            {
                ProductInStockId = productStockId,
                ProductId = productId,
                Stock = 1
            });

            context.SaveChanges();

            ProductInStockUpdateStockEventHandler handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem
                    {
                        ProductId = productId,
                        Stock = 2,
                        Action = Common.ProductInStockAction.Add
                    }
                }
            }, new CancellationToken()).Wait();

            int stockInDb = context.Stocks.Single(item => item.ProductId == productId).Stock;

            Assert.AreEqual(stockInDb, 3);
        }
        
        
        [TestMethod]
        public void TryToAddStockWhenProductNotExists()
        {
            ApplicationDBContext context = ApplicationDbContextInMemory.Get();

            int productId = 4;
            
            ProductInStockUpdateStockEventHandler handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem
                    {
                        ProductId = productId,
                        Stock = 2,
                        Action = Common.ProductInStockAction.Add
                    }
                }
            }, new CancellationToken()).Wait();

            var stockInDb = context.Stocks.Single(item => item.ProductId == productId).Stock;

            Assert.AreEqual(stockInDb, 2);
        }
    }
}