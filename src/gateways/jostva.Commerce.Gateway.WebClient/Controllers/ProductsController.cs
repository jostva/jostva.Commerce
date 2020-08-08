using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Catalog.DTOs;
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
    public class ProductsController : ControllerBase
    {
        private readonly ICatalogProxy catalogProxy;

        public ProductsController(ICatalogProxy catalogProxy
        )
        {
            this.catalogProxy = catalogProxy;
        }


        [HttpGet]
        public async Task<DataCollection<ProductDto>> GetAll(int page, int take)
        {
            return await catalogProxy.GetAllAsync(page, take);
        }


        [HttpGet("{id}")]
        public async Task<ProductDto> Get(int id)
        {
            return await catalogProxy.GetAsync(id);
        }
    }
}