using Microsoft.Extensions.DependencyInjection;
using Star.WebApp.MVC.Services;

namespace Star.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticateService, AuthenticateService>();
        }
    }
}