#region usings

using jostva.Commerce.Customer.Service.EventHandlers.Commands;
using jostva.Commerce.Customer.Service.Queries.DTOs;
using jostva.Commerce.Customer.Service.Queries.Interfaces;
using jostva.Infrastructure.Collection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Customer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> logger;
        private readonly IMediator mediator;
        private readonly IClientQueryService clientQuerService;


        public ClientsController(ILogger<ClientsController> logger,
                                IMediator mediator,
                                IClientQueryService clientQuerService)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.clientQuerService = clientQuerService;
        }


        [HttpGet]
        public async Task<DataCollection<ClientDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> clients = null;

            if (!string.IsNullOrEmpty(ids))
            {
                clients = ids.Split(',').Select(x => Convert.ToInt32(x));
            }

            return await clientQuerService.GetAllAsync(page, take, clients);
        }


        [HttpGet("{id}")]
        public async Task<ClientDto> Get(int id)
        {
            return await clientQuerService.GetAsync(id);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateCommand notification)
        {
            await mediator.Publish(notification);
            return Ok();
        }
    }
}