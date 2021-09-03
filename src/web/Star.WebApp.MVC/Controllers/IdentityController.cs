using Microsoft.AspNetCore.Mvc;
using Star.WebApp.MVC.Models.User;
using Star.WebApp.MVC.Services;
using System.Threading.Tasks;

namespace Star.WebApp.MVC.Controllers
{
    public class IdentityController : Controller
    {

        private readonly IAuthenticateService _authenticateService;

        public IdentityController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

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

            var response = await _authenticateService.Register(userRegister);


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
            var response = await _authenticateService.Login(userLogin);

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
