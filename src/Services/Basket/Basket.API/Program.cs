using Basket.API;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IBasketRepsitoy, BasketRepsitoy>();
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = $"{EnvironmentVariable.RedisConnection}:{EnvironmentVariable.RedisPort}";
});
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Basket.API", Version = "V1" });

});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/V1/swagger.json", "Basket.API V1");
});
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});