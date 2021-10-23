using FluentValidation.Results;
using MediatR;
using Star.Core.Messages;
using System.Threading.Tasks;

namespace Star.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        public readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }

        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}