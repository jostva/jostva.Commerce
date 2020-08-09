using jostva.Commerce.Gateway.Models;
using jostva.Commerce.Gateway.Models.Order.DTOs;
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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IOrderProxy orderProxy;


        public DataCollection<OrderDto> Orders { get; set; }
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;


        public IndexModel(
            ILogger<IndexModel> logger,
            IOrderProxy orderProxy
        )
        {
            this.orderProxy = orderProxy;
            this.logger = logger;
        }


        public async Task OnGet()
        {
            Orders = await orderProxy.GetAllAsync(CurrentPage, 10);
        }
    }
}