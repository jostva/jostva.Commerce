#region usings

using HealthChecks.UI.Client;
using jostva.Commerce.Order.Data;
using jostva.Commerce.Order.Service.Proxies;
using jostva.Commerce.Order.Service.Proxies.Catalog;
using jostva.Commerce.Order.Service.Proxies.Catalog.Interfaces;
using jostva.Commerce.Order.Service.Queries;
using jostva.Commerce.Order.Service.Queries.Interfaces;
using jostva.Infrastructure.Logging;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

#endregion

namespace jostva.Commerce.Order.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //  HttpContextAccessor
            services.AddHttpContextAccessor();

            //  Db Context
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection"),
                        item => item.MigrationsHistoryTable("__EFMigrationsHistory", "Order")
                    )
            );

            //  Heatlh Checks
            services.AddHealthChecks()
                    .AddCheck("self", () => HealthCheckResult.Healthy())
                    .AddDbContextCheck<ApplicationDbContext>();

            services.AddHealthChecksUI();

            // Api Urls
            services.Configure<ApiUrls>(opts => Configuration.GetSection("ApiUrls").Bind(opts));

            // Azure Service Bus ConnectionString
            services.Configure<AzureServiceBus>(opts => Configuration.GetSection("AzureServiceBus").Bind(opts));

            // Proxies
            services.AddHttpClient<ICatalogProxy, CatalogHttpProxy>();
            //services.AddTransient<ICatalogProxy, CatalogQueueProxy>();

            //  Event handlers
            services.AddMediatR(Assembly.Load("jostva.Commerce.Order.Service.EventHandlers"));

            // Query services
            services.AddTransient<IOrderQueryService, OrderQueryService>();

            // API Controllers
            services.AddControllers();

            // Add Authentication
            var secretKey = Encoding.ASCII.GetBytes(
                Configuration.GetValue<string>("SecretKey")
            );

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                loggerFactory.AddSyslog(
                    Configuration.GetValue<string>("Papertrail:host"),
                    Configuration.GetValue<int>("Papertrail:port"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecksUI();
                endpoints.MapControllers();
            });
        }
    }
}