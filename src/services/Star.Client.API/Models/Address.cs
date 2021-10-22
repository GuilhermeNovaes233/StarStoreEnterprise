using Star.Core.DomainObjects;
using System;

namespace Star.Client.API.Models
{
    public class Address : Entity
    {
        //EF Relation
        public Client Client { get; protected set; }

        public Address(string street, string number, string complement, string neighborhood, string zipCode, string city, string state, Guid clientId)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            ZipCode = zipCode;
            City = city;
            State = state;
            ClientId = clientId;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Guid ClientId { get; private set; }
    }
}