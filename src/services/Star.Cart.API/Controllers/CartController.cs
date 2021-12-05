using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Star.WebApp.MVC.Controllers;

namespace Star.Cart.API.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
