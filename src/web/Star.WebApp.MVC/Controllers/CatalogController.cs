using Microsoft.AspNetCore.Mvc;
using Star.WebApp.MVC.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Star.WebApp.MVC.Controllers
{
    public class CatalogController : Controller
    {
        public readonly ICatalogService _catalogService;
        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("window")]
        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.GetAll();
            return View(products);
        }

        [HttpGet]
        [Route("product-detail/{id}")]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var product = await _catalogService.GetById(id);
            return View(product);
        }
    }
}