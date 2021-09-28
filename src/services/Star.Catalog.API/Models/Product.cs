using Star.Core.DomainObjects;
using System;

namespace Star.Catalog.API.Models
{
    public class Product : Entity<Guid>, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Value { get; set; }
        public DateTime DataRegister { get; set; }
        public string Image { get; set; }
        public int QuantityStock { get; set; }
    }
}