using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Star.WebApp.MVC.Models.User;
using Star.WebApp.MVC.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

            //Realizar Login
            await Login(response);

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

            //if (false) return View(userLogin);

            //Realizar Login na APP
            await Login(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            //Limpar Cookie
            return RedirectToAction("Index", "Home");
        }

        private async Task Login(UserResponseLogin responseLogin)
        {
            var token = GetFormatToken(responseLogin.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", responseLogin.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );
        }

        private static JwtSecurityToken GetFormatToken(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(jwtToken) as JwtSecurityToken;
        }
    }
}
