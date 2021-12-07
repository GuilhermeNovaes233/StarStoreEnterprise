using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Star.Cart.API.Models;
using System.Threading.Tasks;
using Star.WebApi.Core.Controllers;
using System;
using Star.WebApi.Core.User;
using Star.Cart.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Star.Cart.API.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private readonly IAspNetUser _user;
        private readonly CartContext _context;

        public CartController(IAspNetUser user, CartContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpGet("cart")]
        public async Task<CartClient> GetCart()
        {
            return await GetCartClient() ?? new CartClient();
        }

        [HttpPost("cart")]
        public async Task<CartItem> AddItemCart()
        {
            var cart = await GetCartClient();

            return null;
        }

        private void ManipulateNewCart(CartItem item)
        {
            var cart = new CartClient(_user.GetUserId());

            _context.CartClients.Add(cart);
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

        private async Task<CartClient> GetCartClient() 
        {
            return await _context.CartClients
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.ClientId == _user.GetUserId());
        }
    }
}