using System;

namespace Star.WebApp.MVC.Models.Product
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Value { get; set; }
        public DateTime DateRegister { get; set; }
        public string Image { get; set; }
        public int QuantityStock { get; set; }
    }
}
