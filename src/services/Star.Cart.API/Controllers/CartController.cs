using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Star.Cart.API.Models;
using System.Threading.Tasks;
using Star.WebApi.Core.Controllers;
using System;
using Star.WebApi.Core.User;
using Star.Cart.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public async Task<IActionResult> AddCartItem(CartItem item)
        {
            var cart = await GetCartClient();

            if (cart == null)
                HandleNewCart(item);
            else
                HandleExistingCart(cart, item);

            if (!IsValid()) return CustomResponse();

            await PersistData();
            return CustomResponse();
        }

        [HttpPut("cart/{productId}")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, CartItem item)
        {
            var cart = await GetCartClient();
            var itemcart = await GetCartItemValidated(productId, cart, item);
            if (itemcart == null) return CustomResponse();

            cart.UpdateUnities(itemcart, item.Quantity);

            ValidateCart(cart);
            if (!IsValid()) return CustomResponse();

            _context.CartItems.Update(itemcart);
            _context.CartClients.Update(cart);

            await PersistData();
            return CustomResponse();
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            var cart = await GetCartClient();

            var itemCart = await GetCartItemValidated(productId, cart);
            if (itemCart == null) return CustomResponse();

            ValidateCart(cart);
            if (!IsValid()) return CustomResponse();

            cart.RemoveItem(itemCart);

            _context.CartItems.Remove(itemCart);
            _context.CartClients.Update(cart);

            await PersistData();

            return CustomResponse();
        }

        private async Task<CartClient> GetCartClient()
        {
            return await _context.CartClients
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.ClientId == _user.GetUserId());
        }

        private void HandleNewCart(CartItem item)
        {
            var cart = new CartClient(_user.GetUserId());
            cart.AddItem(item);

            ValidateCart(cart);
            _context.CartClients.Add(cart);
        }

        private void HandleExistingCart(CartClient cart, CartItem item)
        {
            var productItemExists = cart.CartItemExists(item);

            cart.AddItem(item);
            ValidateCart(cart);

            if (productItemExists)
            {
                _context.CartItems.Update(cart.GetByProductId(item.ProductId));
            }
            else
            {
                _context.CartItems.Add(item);
            }

            _context.CartClients.Update(cart);
        }

        private async Task<CartItem> GetCartItemValidated(Guid productId, CartClient cart, CartItem item = null)
        {
            if (item != null && productId != item.ProductId)
            {
                AddErrorProcessing("O item não corresponde ao informado");
                return null;
            }

            if (cart == null)
            {
                AddErrorProcessing("cart não encontrado");
                return null;
            }

            var itemCart = await _context.CartItems
                .FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);

            if (itemCart == null || !cart.CartItemExists(itemCart))
            {
                AddErrorProcessing("O item não está no carrinho");
                return null;
            }

            return itemCart;
        }

        private async Task PersistData()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AddErrorProcessing("Não foi possível persistir os dados no banco");
        }

        private bool ValidateCart(CartClient cart)
        {
            if (cart.IsValid()) return true;

            cart.ValidationResult.Errors.ToList().ForEach(e => AddErrorProcessing(e.ErrorMessage));
            return false;
        }
    }
}