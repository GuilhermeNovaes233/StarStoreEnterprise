using Star.Core.DomainObjects;
using System;

namespace Star.Client.API.Models
{
    public class Client : Entity, IAggregateRoot
    {
        public Client(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = new Email(email);
            Cpf = new Cpf(cpf); ;
            Excluded = false;
        }

        //EF - Relation
        protected Client(){ }

        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Excluded { get; private set; }
        public Address Address { get; private set; }

        public void UpdateEmail(string email)
        {
            Email = new Email(email);
        }

        public void SetAddress(Address address)
        {
            Address = address;
        }
    }
}
