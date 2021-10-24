using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Star.Client.API.Application.Events
{
    public class ClientEventHandler : INotificationHandler<RegisteredClientEvent>
    {
        public Task Handle(RegisteredClientEvent notification, CancellationToken cancellationToken)
        {
            //Enviar evento de confirmação
            return Task.CompletedTask;
        }
    }
}