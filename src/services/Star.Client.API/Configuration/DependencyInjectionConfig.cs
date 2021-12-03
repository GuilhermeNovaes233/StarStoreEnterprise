using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Star.Client.API.Application.Commands;
using Star.Client.API.Application.Events;
using Star.Client.API.Data;
using Star.Client.API.Data.Repository;
using Star.Client.API.Models;
using Star.Client.API.Services;
using Star.Core.Mediator;

namespace Star.Client.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();

            services.AddScoped<INotificationHandler<RegisteredClientEvent>, ClientEventHandler>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ClientContext>();
        }
    }
}