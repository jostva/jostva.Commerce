using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace jostva.Commerce.Catalog.Api.Controllers
{
    [Route("/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> logger;


        public DefaultController(ILogger<DefaultController> logger)
        {
            this.logger = logger;
        }


        [HttpGet]
        public string Get()
        {
            return "Running...";
        }
    }
}