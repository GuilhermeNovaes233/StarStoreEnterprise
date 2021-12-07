using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Star.Cart.API.Models
{
    public class CartClient
    {
        internal const int MAX_QUANTITY_ITEM = 5;

        public CartClient() { }

        public CartClient(Guid clientId)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
        }

        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public decimal AllValue { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public ValidationResult ValidationResult { get; set; }

        internal void CalculateValueCart()
        {
            AllValue = Items.Sum(p => p.CalculateValue());
        }

        internal bool CartItemExists(CartItem item)
        {
            return Items.Any(p => p.ProductId == item.ProductId);
        }

        internal CartItem GetByProductId(Guid ProductId)
        {
            return Items.FirstOrDefault(p => p.ProductId == ProductId);
        }

        internal void AddItem(CartItem item)
        {
            item.AssociateCart(Id);

            if (CartItemExists(item))
            {
                var itemExist = GetByProductId(item.ProductId);
                itemExist.AddUnities(item.Quantity);

                item = itemExist;
                Items.Remove(itemExist);
            }

            Items.Add(item);
            CalculateValueCart();
        }

        internal void UpdateItem(CartItem item)
        {
            item.AssociateCart(Id);

            var itemExists = GetByProductId(item.ProductId);

            Items.Remove(itemExists);
            Items.Add(item);

            CalculateValueCart();
        }

        internal void UpdateUnities(CartItem item, int unidades)
        {
            item.UpdateUnities(unidades);
            UpdateItem(item);
        }

        internal void RemoveItem(CartItem item)
        {
            Items.Remove(GetByProductId(item.ProductId));
            CalculateValueCart();
        }

        internal bool IsValid()
        {
            var errors = Items.SelectMany(i => new CartItem.ItemCartValidation().Validate(i).Errors).ToList();
            errors.AddRange(new CartClientValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(errors);

            return ValidationResult.IsValid;
        }

        public class CartClientValidation : AbstractValidator<CartClient>
        {
            public CartClientValidation()
            {
                RuleFor(c => c.ClientId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Cliente não reconhecido");

                RuleFor(c => c.Items.Count)
                    .GreaterThan(0)
                    .WithMessage("O carrinho não possui Items");

                RuleFor(c => c.AllValue)
                    .GreaterThan(0)
                    .WithMessage("O valor total do carrinho precisa ser maior que 0");
            }
        }
    }
} 