using Catalog.API.Data;
using Catalog.API.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Catalog.API", Version = "V1" });

});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
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