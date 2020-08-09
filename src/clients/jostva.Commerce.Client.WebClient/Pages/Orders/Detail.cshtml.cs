using jostva.Commerce.Gateway.Models.Order.DTOs;
using jostva.Commerce.Gateway.WebClient.Proxy.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace jostva.Commerce.Client.WebClient.Pages.Orders
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class DetailModel : PageModel
    {
        private readonly ILogger<DetailModel> logger;
        private readonly IOrderProxy orderProxy;

        public OrderDto Order { get; set; }

        public DetailModel(
            ILogger<DetailModel> logger,
            IOrderProxy orderProxy
        )
        {
            this.orderProxy = orderProxy;
            this.logger = logger;
        }

        public async Task OnGet(int id)
        {
            Order = await orderProxy.GetAsync(id);
        }
    }
}