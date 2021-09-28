using Microsoft.Extensions.DependencyInjection;
using Star.Catalog.API.Data;
using Star.Catalog.API.Data.Repository;
using Star.Catalog.API.Models;

namespace Star.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}
