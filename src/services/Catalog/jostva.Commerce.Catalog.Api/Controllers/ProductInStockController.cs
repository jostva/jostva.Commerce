#region usings

using jostva.Commerce.Catalog.Services.EventHandlers.Commands;
using jostva.Commerce.Catalog.Services.Queries.DTOs;
using jostva.Commerce.Catalog.Services.Queries.Interfaces;
using jostva.Infrastructure.Collection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Catalog.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInStockController : ControllerBase
    {
        #region attributes

        private readonly ILogger<ProductInStockController> logger;
        private readonly IMediator mediator;
        private readonly IProductInStockQueryService productInStockQueryService;

        #endregion

        #region constructor

        public ProductInStockController(ILogger<ProductInStockController> logger,
                                        IMediator mediator,
                                        IProductInStockQueryService productInStockQueryService)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.productInStockQueryService = productInStockQueryService;
        }

        #endregion

        #region methods

        [HttpGet]
        public async Task<DataCollection<ProductInStockDto>> GetAll(int page = 1, int take = 10, string products = null)
        {
            IEnumerable<int> ids = null;

            if (!string.IsNullOrEmpty(products))
            {
                ids = products.Split(',').Select(x => Convert.ToInt32(x));
            }

            return await productInStockQueryService.GetAllAsync(page, take, ids);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateStock(ProductInStockUpdateStockCommand command)
        {
            await mediator.Publish(command);
            return NoContent();
        }

        #endregion
    }
}