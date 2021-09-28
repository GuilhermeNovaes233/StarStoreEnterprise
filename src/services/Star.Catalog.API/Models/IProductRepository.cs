using Star.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Star.Catalog.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);

        void AddProduct(Product product);
        void UpdateProduct(Product product);
    }
}