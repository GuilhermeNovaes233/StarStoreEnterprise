using Star.WebApp.MVC.Models.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Star.WebApp.MVC.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAll();

        Task<ProductViewModel> GetById(Guid id);
    }
}
