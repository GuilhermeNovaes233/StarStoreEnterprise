using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Star.Client.API.Application.Commands;
using Star.Core.Mediator;

namespace Star.Client.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();
        }
    }
}