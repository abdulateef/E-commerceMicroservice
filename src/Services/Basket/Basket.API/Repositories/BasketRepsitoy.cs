using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepsitoy : IBasketRepsitoy
    {
        private readonly IDistributedCache _distributedCache;
        public BasketRepsitoy(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache ?? throw new ArgumentException(nameof(distributedCache));
        }

        public async Task<bool> DeletBasket(string username)
        {
            try
            {
                await _distributedCache.RemoveAsync(username);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ShoppingCart> GetBasket(string username)
        {
            try
            {
                var basket = await _distributedCache.GetStringAsync(username);
                if (string.IsNullOrEmpty(basket))
                    return null;
                return JsonConvert.DeserializeObject<ShoppingCart>(basket);
            }
            catch (Exception ex)
            { 
                return null;
            }
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            try
            {
                await _distributedCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
                return basket;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
