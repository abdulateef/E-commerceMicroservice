

using Catalog.API.Data;
using Catalog.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace Catalog.API
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
        //.AddJsonOptions(options =>
        //{
        //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
        //});

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Catalog.API", Version = "V1" });
                });
           // services.AddHttpContextAccessor();
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddOptions();
			//services.AddMemoryCache();
            //services.AddMvc(o => o.EnableEndpointRouting = false);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
  
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/V1/swagger.json", "My API");

            });

            app.UseHttpsRedirection();
            app.UseRouting();
			// global cors policy
			app.UseCors(x => x
				.AllowAnyMethod()
				.AllowAnyHeader()
				.SetIsOriginAllowed(origin => true) // allow any origin
				.AllowCredentials()); // allow credentials

			//app.UseAuthentication();
           // app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            { 
                endpoints.MapControllers();
            });
        }


    }
}