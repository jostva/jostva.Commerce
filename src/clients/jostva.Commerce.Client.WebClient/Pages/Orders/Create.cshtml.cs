using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Catalog.DTOs;
using jostva.Commerce.Gateway.Models.Customer.DTOs;
using jostva.Commerce.Gateway.Models.Order.Commands;
using jostva.Commerce.Gateway.WebClient.Proxy.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace jostva.Commerce.Client.WebClient.Pages.Orders
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CreateModel : PageModel
    {
        private readonly ILogger<CreateModel> logger;
        private readonly IOrderProxy orderProxy;
        private readonly IClientProxy clientProxy;
        private readonly IProductProxy productProxy;

        public DataCollection<ProductDto> Products { get; set; }

        public DataCollection<ClientDto> Clients { get; set; }


        public CreateModel(ILogger<DetailModel> logger,
                           IOrderProxy orderProxy,
                           IClientProxy clientProxy,
                           IProductProxy productProxy
        )
        {
            this.orderProxy = orderProxy;
            this.clientProxy = clientProxy;
            this.productProxy = productProxy;
        }

        public async Task OnGet()
        {
            // *** Lo ideal sería implementar un Autocomplete para buscar los productos y cliente a demanda
            Products = await productProxy.GetAllAsync(1, 100);
            Clients = await clientProxy.GetAllAsync(1, 100);
        }

        public async Task<IActionResult> OnPost([FromBody] OrderCreateCommand command)
        {
            await orderProxy.CreateAsync(command);
            return StatusCode(200);
        }
    }
}