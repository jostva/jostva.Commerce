using jostva.Commerce.Identity.Domain;
using jostva.Commerce.Identity.Services.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace jostva.Commerce.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> logger;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMediator mediator;


        public IdentityController(ILogger<IdentityController> logger,
                                  SignInManager<ApplicationUser> signInManager,
                                  IMediator mediator)
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserCreateCommand command)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await mediator.Send(command);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok();
            }

            return BadRequest();
        }


        [HttpPost("authentication")]
        public async Task<IActionResult> Authentication(UserLoginCommand command)
        {
            if (ModelState.IsValid)
            {
                Services.EventHandlers.Responses.IdentityAccess result = await mediator.Send(command);

                if (!result.Succeeded)
                {
                    return BadRequest("Access denied");
                }

                return Ok(result);
            }

            return BadRequest();
        }
    }
}