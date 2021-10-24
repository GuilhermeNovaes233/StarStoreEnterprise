using Star.Core.Messages;
using System;

namespace Star.Client.API.Application.Events
{
    public class RegisteredClientEvent : Event
    {
        public RegisteredClientEvent(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
    }
}
