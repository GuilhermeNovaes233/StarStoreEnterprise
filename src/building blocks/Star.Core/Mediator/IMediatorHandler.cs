using FluentValidation.Results;
using Star.Core.Messages;
using System.Threading.Tasks;

namespace Star.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}