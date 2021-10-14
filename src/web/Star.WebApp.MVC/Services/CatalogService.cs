using Microsoft.Extensions.Options;
using Star.WebApp.MVC.Extensions;
using Star.WebApp.MVC.Models.Product;
using Star.WebApp.MVC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Star.WebApp.MVC.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);

            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {

            var response = await _httpClient.GetAsync($"catalog/products");

            HandleErrorsResponse(response);

            return await DeserializeObjectResponse<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"catalog/products/{id}");

            HandleErrorsResponse(response);

            return await DeserializeObjectResponse<ProductViewModel>(response);
        }
    }
}