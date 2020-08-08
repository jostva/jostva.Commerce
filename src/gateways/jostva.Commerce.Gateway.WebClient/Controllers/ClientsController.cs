using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Customer.DTOs;
using jostva.Commerce.Gateway.Proxies.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace jostva.Commerce.Gateway.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ICustomerProxy customerProxy;


        public ClientsController(ICustomerProxy customerProxy
        )
        {
            this.customerProxy = customerProxy;
        }


        [HttpGet]
        public async Task<DataCollection<ClientDto>> GetAll(int page, int take)
        {
            return await customerProxy.GetAllAsync(page, take);
        }


        [HttpGet("{id}")]
        public async Task<ClientDto> Get(int id)
        {
            return await customerProxy.GetAsync(id);
        }
    }
}