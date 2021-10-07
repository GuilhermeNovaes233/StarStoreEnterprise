using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Star.Catalog.API.Models;
using Star.WebApi.Core.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Star.Catalog.API.Controllers
{
    [ApiController]
    [Authorize]
    public class CatalogController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        [AllowAnonymous]
        [HttpGet("catalog/products")]
        public async Task<IEnumerable<Product>> Index()
        {
            return await _productRepository.GetAll(); 
        }

        [ClaimsAuthorize("Catalog", "Read")]
        [HttpGet("catalog/products/{id}")]
        public async Task<Product> ProductDetails(Guid id)
        {
            return await _productRepository.GetById(id);
        }
    }
}