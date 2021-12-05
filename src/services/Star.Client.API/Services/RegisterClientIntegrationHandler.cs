using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Star.Client.API.Application.Commands;
using Star.Core.Mediator;
using Star.Core.Messages.Integration;
using Star.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Star.Client.API.Services
{
    public class RegisterClientIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisterClientIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request =>
               await RegisterClient(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegisterClient(UserRegisteredIntegrationEvent message)
        {
            var clientCommand = new RegisterClientCommand(message.Id, message.Name, message.Email, message.Cpf);

            ValidationResult success;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                success = await mediator.SendCommand(clientCommand);
            }
            
            return new ResponseMessage(success);
        }
    }
}
