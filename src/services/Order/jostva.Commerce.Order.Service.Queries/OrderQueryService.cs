#region usings

using jostva.Commerce.Order.Data;
using jostva.Commerce.Order.Service.Queries.DTOs;
using jostva.Commerce.Order.Service.Queries.Interfaces;
using jostva.Infrastructure.Collection;
using jostva.Infrastructure.Mapping;
using jostva.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Order.Service.Queries
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly ApplicationDbContext context;


        public OrderQueryService(ApplicationDbContext context)
        {
            this.context = context;        
        }


        public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take)
        {
            var collection = await context.Orders
                                          .Include(x => x.Items)
                                          .OrderByDescending(x => x.OrderId)
                                          .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<OrderDto>>();
        }


        public async Task<OrderDto> GetAsync(int id)
        {
            return (await context.Orders.Include(x => x.Items).SingleAsync(x => x.OrderId == id)).MapTo<OrderDto>();
        }
    }
}