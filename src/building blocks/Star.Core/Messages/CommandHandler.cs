using FluentValidation.Results;
using Star.Core.Data;
using System.Threading.Tasks;

namespace Star.Core.Messages
{
    public class CommandHandler
    {
        protected ValidationResult ValidationResult;

        public CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddErrors(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> SaveChangesAsync(IUnitOfWork uow)
        {
            if (!await uow.Commit()) AddErrors("Houve um erro ao persistir os dados");

            return ValidationResult;
        }
    }
}