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
    public class ProductsController : ControllerBase
    {
        #region attributes

        private readonly ILogger<DefaultController> logger;
        private readonly IProductQueryService productQueryService;
        private readonly IMediator mediator;

        #endregion

        #region constructor

        public ProductsController(ILogger<DefaultController> logger, IProductQueryService productQueryService, IMediator mediator)
        {
            this.logger = logger;
            this.productQueryService = productQueryService;
            this.mediator = mediator;
        }

        #endregion

        #region methods

        [HttpGet]
        public async Task<ActionResult<DataCollection<ProductDto>>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> products = null;
            if (!string.IsNullOrEmpty(ids))
            {
                products = ids.Split(',').Select(item => Convert.ToInt32(item));
            }

            return await productQueryService.GetAllAsync(page, take, products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            return await productQueryService.GetAsync(id);
        }


        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(ProductCreateCommand command)
        {
            await mediator.Publish(command);
            return Ok();    //TODO: Corregir
        }

        #endregion
    }
}