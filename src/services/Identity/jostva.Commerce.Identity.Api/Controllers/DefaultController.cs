using Microsoft.AspNetCore.Mvc;

namespace jostva.Commerce.Identity.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "Runnig";
        }
    }
}