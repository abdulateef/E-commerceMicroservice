using Basket.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IBasketRepsitoy, BasketRepsitoy>();
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = $"{EnvironmentVariable.RedisConnection}:{EnvironmentVariable.RedisPort}";
            });
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Basket.API", Version = "V1" });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI( opt =>
            {
                opt.SwaggerEndpoint("/swagger/V1/swagger.json", "Basket.API V1");
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
