using System;

namespace Catalog.API
{
    public static class EnvironmentVariable
    {
        public static string MongoDbConnectionString { get; } = Environment.GetEnvironmentVariable("MongoDbConnectionString");
        public static string DatabaseName { get; } = Environment.GetEnvironmentVariable("DatabaseName");
        public static string CollectionName { get; } = Environment.GetEnvironmentVariable("CollectionName");


    }
}
