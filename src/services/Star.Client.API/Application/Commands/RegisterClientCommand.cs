using FluentValidation;
using Star.Core.Messages;
using System;

namespace Star.Client.API.Application.Commands
{
    public class RegisterClientCommand : Command
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegisterClientCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarClienteValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    public class RegistrarClienteValidation : AbstractValidator<RegisterClientCommand>
    {
        public RegistrarClienteValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado");

            RuleFor(c => c.Cpf)
                .Must(CpfIsValid)
                .WithMessage("O CPF informado não é válido.");

            RuleFor(c => c.Email)
                .Must(EmailIsValid)
                .WithMessage("O e-mail informado não é válido.");
        }

        protected static bool CpfIsValid(string cpf)
        {
            return Core.DomainObjects.Cpf.Validate(cpf);
        }

        protected static bool EmailIsValid(string email)
        {
            return Core.DomainObjects.Email.Validate(email);
        }
    }
}