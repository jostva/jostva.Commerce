#region usings

using jostva.Commerce.Catalog.Data;
using jostva.Commerce.Catalog.Services.Queries.DTOs;
using jostva.Commerce.Catalog.Services.Queries.Interfaces;
using jostva.Infrastructure.Collection;
using jostva.Infrastructure.Mapping;
using jostva.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Catalog.Services.Queries
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly ApplicationDBContext context;


        public ProductQueryService(ApplicationDBContext context)
        {
            this.context = context;
        }


        public async Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int> products = null)
        {
            DataCollection<Domain.Product> collection = await context.Products
                                                                     .Where(x => products == null || products.Contains(x.ProductId))
                                                                     .OrderBy(x => x.Name)
                                                                     .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<ProductDto>>();
        }


        public async Task<ProductDto> GetAsync(int id)
        {
            return (await context.Products.SingleAsync(x => x.ProductId == id)).MapTo<ProductDto>();
        }
    }
}