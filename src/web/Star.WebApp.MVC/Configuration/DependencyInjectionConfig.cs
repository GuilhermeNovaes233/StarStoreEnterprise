using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Star.WebApp.MVC.Extensions;
using Star.WebApp.MVC.Services;
using Star.WebApp.MVC.Services.Handlers;
using Star.WebApp.MVC.Services.Interfaces;
using System;

namespace Star.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();


            services.AddHttpClient<IAuthenticateService, AuthenticateService>();


            var retryWaitPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[] {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                },
                onRetry: (outcome, timespan, retrycount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Tentando pela {retrycount} vez!");
                }); 

            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(retryWaitPolicy);
                //.AddTransientHttpErrorPolicy(
                //    p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));


            //services.AddHttpClient("Refit",
            //        options =>
            //        {
            //            options.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value);
            //        })
            //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //    .AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}