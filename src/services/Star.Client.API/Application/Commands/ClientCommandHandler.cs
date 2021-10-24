using FluentValidation.Results;
using MediatR;
using Star.Client.API.Application.Events;
using Star.Client.API.Models;
using Star.Core.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Star.Client.API.Application.Commands
{
    public class ClientCommandHandler : CommandHandler, IRequestHandler<RegisterClientCommand, ValidationResult>
    {
        private readonly IClientRepository _clientRepository;

        public ClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var client = new Models.Client(message.Id, message.Name, message.Email, message.Cpf);

            var clientExists = await _clientRepository.GetByCpf(client.Cpf.Number);
            if(clientExists != null)
            {
                AddErrors("Este CPF já esta em uso");
                return ValidationResult;
            }

            _clientRepository.Add(client);

            client.AddEvent(new RegisteredClientEvent(message.Id, message.Name, message.Email, message.Cpf));

            return await SaveChangesAsync(_clientRepository.UnitOfWork);
        }
    }
}