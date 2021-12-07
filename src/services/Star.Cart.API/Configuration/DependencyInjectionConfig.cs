using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Star.Cart.API.Data;

namespace Star.Cart.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<CartContext>();
        }
    }
}