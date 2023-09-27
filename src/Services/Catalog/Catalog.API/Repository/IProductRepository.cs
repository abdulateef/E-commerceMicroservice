using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<List<Product>> GetProductsByName(string name);
        Task<List<Product>> GetProductsByCategory(string categoryName);

        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);

        Task<bool> DeleteProduct(string id);

    }
}
