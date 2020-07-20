#region usings

using jostva.Commerce.Catalog.Data;
using jostva.Commerce.Catalog.Services.Queries.DTOs;
using jostva.Commerce.Catalog.Services.Queries.Interfaces;
using jostva.Infrastructure.Collection;
using jostva.Infrastructure.Mapping;
using jostva.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Catalog.Services.Queries
{
    public  class ProductInStockQueryService : IProductInStockQueryService
    {
        private readonly ApplicationDBContext context;


        public ProductInStockQueryService(ApplicationDBContext context)
        {
            this.context = context;
        }


        public async Task<DataCollection<ProductInStockDto>> GetAllAsync(int page, int take, IEnumerable<int> products = null)
        {
            var collection = await context.Stocks
                                          .Where(x => products == null || products.Contains(x.ProductId))
                                          .OrderBy(x => x.ProductId).GetPagedAsync(page, take);
                       
           return collection.MapTo<DataCollection<ProductInStockDto>>();
        }
    }
}