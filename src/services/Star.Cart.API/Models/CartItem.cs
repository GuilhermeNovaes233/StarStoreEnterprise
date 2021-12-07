using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace Star.Cart.API.Models
{
    public class CartItem
    {
        public CartItem()
        {
            Id = Guid.NewGuid();
        }    

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public Guid CartId { get; set; }
        [JsonIgnore]
        public CartClient CartClient { get; set; }

        internal void AssociateCart(Guid cartId)
        {
            CartId = cartId;
        }

        internal decimal CalculateValue()
        {
            return Quantity * Value;
        }

        internal void AddUnities(int unidades)
        {
            Quantity += unidades;
        }

        internal void UpdateUnities(int unidades)
        {
            Quantity = unidades;
        }

        internal bool IsValid()
        {
            return new ItemCartValidation().Validate(this).IsValid;
        }

        public class ItemCartValidation : AbstractValidator<CartItem>
        {
            public ItemCartValidation()
            {
                RuleFor(c => c.ProductId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do produto inválido");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do produto não foi informado");

                RuleFor(c => c.Quantity)
                    .GreaterThan(0)
                    .WithMessage(item => $"A quantidade miníma para o {item.Name} é 1");

                RuleFor(c => c.Quantity)
                    .LessThanOrEqualTo(CartClient.MAX_QUANTITY_ITEM)
                    .WithMessage(item => $"A quantidade máxima do {item.Name} é {CartClient.MAX_QUANTITY_ITEM}");

                RuleFor(c => c.Value)
                    .GreaterThan(0)
                    .WithMessage(item => $"O valor do {item.Name} precisa ser maior que 0");
            }
        }
    }
}