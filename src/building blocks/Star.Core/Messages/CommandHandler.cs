using FluentValidation.Results;

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
    }
}