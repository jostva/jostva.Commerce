﻿#region usings

using jostva.Commerce.Catalog.Services.Queries.DTOs;
using jostva.Infrastructure.Collection;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Catalog.Services.Queries.Interfaces
{
    public interface IProductInStockQueryService
    {

        Task<DataCollection<ProductInStockDto>> GetAllAsync(int page, int take, IEnumerable<int> products = null);
    }
}