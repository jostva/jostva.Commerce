#region usings

using jostva.Commerce.Client.Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Client.Authentication.Pages
{
    public class IndexModel : PageModel
    {
        #region attributes

        private readonly ILogger<IndexModel> logger;
        private readonly string identityUrl;

        #endregion

        #region properties

        [BindProperty(SupportsGet = true)]
        public string ReturnBaseUrl { get; set; }

        [BindProperty]
        public LoginViewModel model { get; set; }

        public bool HasInvalidAccess { get; set; }

        #endregion

        #region constructor

        public IndexModel(ILogger<IndexModel> logger,
                          IConfiguration configuration)
        {
            this.logger = logger;
            identityUrl = configuration.GetValue<string>("IdentityUrl");
        }

        #endregion

        #region methods

        public void OnGet()
        {

        }


        public async Task<IActionResult> OnPost()
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonSerializer.Serialize(model),
                                                Encoding.UTF8,
                                                "application/json"
                );

                var request = await client.PostAsync(identityUrl + "api/identity/authentication", content);

                if (!request.IsSuccessStatusCode)
                {
                    HasInvalidAccess = true;
                    return Page();
                }

                var result = JsonSerializer.Deserialize<IdentityAccess>(
                    await request.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return Redirect(ReturnBaseUrl + $"account/connect?access_token={result.AccessToken}");
            }
        }

        #endregion
    }
}