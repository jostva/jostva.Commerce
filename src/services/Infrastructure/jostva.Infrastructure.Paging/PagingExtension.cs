#region usings

using jostva.Infrastructure.Collection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks; 

#endregion

namespace jostva.Infrastructure.Paging
{
    public static class PagingExtension
    {
        public static async Task<DataCollection<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int take)
        {
            int originalPages = page;

            page--;

            if (page > 0)
            {
                page *= take;
            }

            DataCollection<T> result = new DataCollection<T>
            {
                Items = await query.Skip(page)
                                   .Take(take)
                                   .ToListAsync(),
                Total = await query.CountAsync(),
                Page = originalPages
            };

            if (result.Total > 0)
            {
                result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
            }

            return result;
        }
    }
}