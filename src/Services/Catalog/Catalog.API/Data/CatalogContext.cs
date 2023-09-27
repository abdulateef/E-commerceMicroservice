using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext()
        {
            var client = new MongoClient(EnvironmentVariable.MongoDbConnectionString);
            var database = client.GetDatabase(EnvironmentVariable.DatabaseName);
            Products = database.GetCollection<Product>(EnvironmentVariable.CollectionName);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
