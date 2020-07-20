using jostva.Commerce.Order.Service.EventHandlers.Commands;
using jostva.Commerce.Order.Service.Queries.DTOs;
using jostva.Commerce.Order.Service.Queries.Interfaces;
using jostva.Infrastructure.Collection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace jostva.Commerce.Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;
        private readonly IMediator mediator;
        private readonly IOrderQueryService orderQueryService;


        public OrdersController(ILogger<OrdersController> logger,
                                IMediator mediator,
                                IOrderQueryService orderQueryService)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.orderQueryService = orderQueryService;
        }


        [HttpGet]
        public async Task<DataCollection<OrderDto>> GetAll(int page = 1, int take = 10)
        {
            return await orderQueryService.GetAllAsync(page, take);
        }


        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            return await orderQueryService.GetAsync(id);
        }


        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand notification)
        {
            await mediator.Publish(notification);
            return Ok();
        }
    }
}