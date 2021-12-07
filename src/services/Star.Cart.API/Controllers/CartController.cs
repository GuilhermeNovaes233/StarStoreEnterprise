using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Star.Cart.API.Models;
using System.Threading.Tasks;
using Star.WebApi.Core.Controllers;
using System;

namespace Star.Cart.API.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
       [HttpGet("cart")]
       public async Task<CartItem> GetCart()
       {
            return null;
       }

        [HttpPost("cart")]
        public async Task<CartItem> AddItemCart()
        {
            return null;
        }

        [HttpPut("cart/{productId}")]
        public async Task<CartItem> UpdateItemCart(Guid productId, CartItem item)
        {
            return null;
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> DeleteItemCart(Guid productId)
        {
            return null;
        }
    }
}
