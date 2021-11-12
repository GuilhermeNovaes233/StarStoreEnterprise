using FluentValidation.Results;

namespace Star.Core.Messages.Integration
{
    //Mensagem de volta
    public class ResponseMessage : Message
    {
        public ResponseMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get; set; }
    }
}