using FluentValidation.Results;
using MediatR;
using Star.Core.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Star.Client.API.Application.Commands
{
    public class ClientCommandHandler : CommandHandler, IRequestHandler<RegisterClientCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var client = new Models.Client(message.Id, message.Name, message.Email, message.Cpf);

            return message.ValidationResult;
        }
    }
}