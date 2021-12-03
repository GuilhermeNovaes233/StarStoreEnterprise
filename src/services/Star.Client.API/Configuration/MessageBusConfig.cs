using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Star.Client.API.Services;
using Star.Core.Utils;
using Star.MessageBus;

namespace Star.Client.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
                IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegisterClientIntegrationHandler>();
        }
    }
}