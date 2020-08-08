using jostva.Commerce.Gateway.Proxies;
using jostva.Commerce.Gateway.Proxies.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace jostva.Commerce.Gateway.WebClient.Config
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddAppsettingBinding(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<ApiUrls>(opts => configuration.GetSection("ApiUrls").Bind(opts));
            return service;
        }


        public static IServiceCollection AddProxiesRegistration(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpContextAccessor();

            service.AddHttpClient<IOrderProxy, OrderProxy>();
            service.AddHttpClient<ICustomerProxy, CustomerProxy>();
            service.AddHttpClient<ICatalogProxy, CatalogProxy>();

            return service;
        }
    }
}