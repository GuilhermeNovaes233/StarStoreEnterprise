using System;

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
        public CartClient CartClient { get; set; }
    }
}