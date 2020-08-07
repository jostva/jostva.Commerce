using jostva.Commerce.Identity.Services.Queries.DTOs;
using jostva.Commerce.Identity.Services.Queries.Interfaces;
using jostva.Infrastructure.Collection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jostva.Commerce.Identity.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IMediator mediator;
        private readonly IUserQueryService userQueryService;


        public UserController(ILogger<UserController> logger,
                              IMediator mediator,
                              IUserQueryService userQueryService)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.userQueryService = userQueryService;
        }


        [HttpGet]
        public async Task<DataCollection<UserDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<string> users = ids?.Split(',');
            return await userQueryService.GetAllAsync(page, take, users);
        }


        [HttpGet("{id}")]
        public async Task<UserDto> Get(string id)
        {
            return await userQueryService.GetAsync(id);
        }
    }
}