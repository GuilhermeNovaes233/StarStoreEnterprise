using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Star.WebApp.MVC.Extensions;
using Star.WebApp.MVC.Services;

namespace Star.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticateService, AuthenticateService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}