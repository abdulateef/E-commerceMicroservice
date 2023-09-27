using Catalog.API.Entities;
using Catalog.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class CatalogController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet("id:lenght(24)", Name = "getById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            try
            {
                var product = await _productRepository.GetProduct(id);
                if (product != null)
                {
                    return Ok(product);
                }
                _logger.LogError($"product not found");
                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("categoryName:lenght(100)", Name = "getByCategoryName")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategoryName(string categoryName)
        {
            try
            {
                var products = await _productRepository.GetProductsByName(categoryName);
                if (products.Count() > 0)
                {
                    return Ok(products);
                }
                _logger.LogError($"product not found");
                  return Ok(new List<Product>());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            try
            {
                var products = await _productRepository.GetProducts();
                if (products.Count() > 0)
                {
                    return Ok(products);
                }
                _logger.LogError($"product not found");
                return Ok(new List<Product>());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> Create(Product product )
        {
            try
            {
                var createProduct = await _productRepository.CreateProduct(product);
                if (createProduct! != null)
                {
                    return Ok(createProduct);
                }
                _logger.LogError($"product creation failed");
                return BadRequest("product creation failed");
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpDelete("delete")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Delete(string id)
        {
            try
            {
                var createProduct = await _productRepository.DeleteProduct(id);
                if (createProduct)
                {
                    return Ok(createProduct);
                }
                _logger.LogError($"product deleteion failed");
                return BadRequest("failed");
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPut("update")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UPdate(Product product)
        {
            try
            {
                var createProduct = await _productRepository.UpdateProduct(product);
                if (createProduct)
                {
                    return Ok(createProduct);
                }
                _logger.LogError($"product deleteion failed");
                return BadRequest("failed");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
