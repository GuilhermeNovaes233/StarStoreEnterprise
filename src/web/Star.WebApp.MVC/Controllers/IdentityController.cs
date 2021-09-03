using Microsoft.AspNetCore.Mvc;
using Star.WebApp.MVC.Models.User;
using System.Threading.Tasks;

namespace Star.WebApp.MVC.Controllers
{
    public class IdentityController : Controller
    {
        [HttpGet("new-account")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("new-account")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid)
                return View(userRegister);

            //API - Registro
            if (false) return View(userRegister);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return View(userLogin);

            //API - Login
            if (false) return View(userLogin);

            //Realizar Login na APP

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            //Limpar Cookie
            return RedirectToAction("Index", "Home");
        }
    }
}
