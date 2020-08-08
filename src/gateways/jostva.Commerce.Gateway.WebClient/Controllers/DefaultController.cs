using Microsoft.AspNetCore.Mvc;

namespace jostva.Commerce.Gateway.WebClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultController : ControllerBase
    {       
     
             [HttpGet]
        public string Get()
        {
            return "Running...";
        }
    }
}