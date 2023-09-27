using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepsitoy _basketRepsitoy;
        private readonly ILogger<BasketController> _logger;
        public BasketController(IBasketRepsitoy basketRepsitoy, ILogger<BasketController> logger)
        {
            _basketRepsitoy = basketRepsitoy;
            _logger = logger;
        }

        [HttpGet("{username}", Name ="getBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {
            try
            {
                var basket = await _basketRepsitoy.GetBasket(username);
                if (basket != null)
                {
                    return Ok(basket);
                }
                _logger.LogError($"cart created");
                return Ok(new ShoppingCart(username));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("updateBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            try
            {
              
                return Ok(await _basketRepsitoy.UpdateBasket(basket));
            }
            catch (Exception)
            {

                throw;
            }
        }



        [HttpDelete("{username}", Name ="deleteBasket")]
        [ProducesResponseType(typeof(Boolean), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> DeeleteBasket(string username)
        {
            try
            {

                return Ok(await _basketRepsitoy.DeletBasket(username));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
