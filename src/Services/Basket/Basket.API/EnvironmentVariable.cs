using System;

namespace Catalog.API
{
    public static class EnvironmentVariable
    {
        public static string RedisConnection { get; } = Environment.GetEnvironmentVariable("RedisConnection");
        public static string RedisPort { get; } = Environment.GetEnvironmentVariable("RedisPort");


    }
}
