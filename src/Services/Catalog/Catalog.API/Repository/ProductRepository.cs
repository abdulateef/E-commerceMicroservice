using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProduct(Product product)  
        {
            try
            {
                 await _context.Products.InsertOneAsync(product);
                return product;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteProduct(string id)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
                var deleteResult = await _context.Products.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Product> GetProduct(string id)
        {
            try
            {
                var filter = Builders<Product>.Filter.Where(p => p.Id.Equals(id));
                var result = await _context.Products.FindAsync(filter);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                var filter = Builders<Product>.Filter.Where(p => true);
                var result =  await _context.Products.FindAsync(filter);
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Product>> GetProductsByCategory(string categoryName)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
                var result = await _context.Products.FindAsync(filter);
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Product>> GetProductsByName(string name)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
                var result = await _context.Products.FindAsync(filter);
                return await result.ToListAsync();
            }
            catch (Exception ex) 
            {

                throw;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                var updateResult = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
