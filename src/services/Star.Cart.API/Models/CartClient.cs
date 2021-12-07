using System;
using System.Collections.Generic;

namespace Star.Cart.API.Models
{
    public class CartClient
    {
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
    }
} 