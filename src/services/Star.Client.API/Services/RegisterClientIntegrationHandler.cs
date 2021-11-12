using EasyNetQ;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Star.Client.API.Application.Commands;
using Star.Core.Mediator;
using Star.Core.Messages.Integration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Star.Client.API.Services
{
    public class RegisterClientIntegrationHandler : BackgroundService
    {
        private IBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisterClientIntegrationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:5672");

            _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request =>
                await RegisterClient(request));

            return Task.CompletedTask;
        }

        private async Task<ResponseMessage> RegisterClient(UserRegisteredIntegrationEvent message)
        {
            var clientCommand = new RegisterClientCommand(message.Id, message.Name, message.Email, message.Cpf);

            ValidationResult success;

            //Service locator
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                success = await mediator.SendCommand(clientCommand);
            }
            
            return new ResponseMessage(success);
        }
    }
}
