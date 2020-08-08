using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Order.Commands;
using jostva.Commerce.Gateway.Models.Order.DTOs;
using jostva.Commerce.Gateway.Proxies.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace jostva.Commerce.Gateway.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProxy orderProxy;
        private readonly ICustomerProxy customerProxy;
        private readonly ICatalogProxy catalogProxy;

        public OrdersController(IOrderProxy orderProxy,
                               ICustomerProxy customerProxy,
                               ICatalogProxy catalogProxy
        )
        {
            this.orderProxy = orderProxy;
            this.customerProxy = customerProxy;
            this.catalogProxy = catalogProxy;
        }


        /// <summary>
        /// Este método no necesita traer la información de los productos porque lo usaremos para solo mostrar
        /// las cabeceras en el listado. RECUERDA: que este API Gateway alimenta a nuestro Web.Client - El backend de nuestro frontend
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet]

        public async Task<DataCollection<OrderDto>> GetAll(int page, int take)
        {
            var result = await orderProxy.GetAllAsync(page, take);

            if (result.HasItems)
            {
                // Retrieve client ids
                var clientIds = result.Items
                    .Select(x => x.ClientId)
                    .GroupBy(g => g)
                    .Select(x => x.Key).ToList();

                var clients = await customerProxy.GetAllAsync(1, clientIds.Count(), clientIds);

                foreach (var order in result.Items)
                {
                    order.Client = clients.Items.Single(x => x.ClientId == order.ClientId);
                }
            }

            return result;
        }


        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            var result = await orderProxy.GetAsync(id);

            // Retrieve client
            result.Client = await customerProxy.GetAsync(result.ClientId);

            // Retrieve product ids
            var productIds = result.Items
                .Select(x => x.ProductId)
                .GroupBy(g => g)
                .Select(x => x.Key).ToList();

            var products = await catalogProxy.GetAllAsync(1, productIds.Count(), productIds);

            foreach (var item in result.Items)
            {
                item.Product = products.Items.Single(x => x.ProductId == item.ProductId);
            }

            return result;
        }


        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand command)
        {
            await orderProxy.CreateAsync(command);
            return Ok();
        }
    }
}