using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.IRepositories;

namespace Talabat.Repository
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDatabase database;

        public BasketRepo(IConnectionMultiplexer redis) // Ask CLR for object from Class implement interface IConnectionMultiplexer
        {
            database = redis.GetDatabase();
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var Basket = await database.StringGetAsync(BasketId);
            return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> CreateUpdateBasketAsync(CustomerBasket Basket)
        {
            var JsonBasket = JsonSerializer.Serialize(Basket);
            // StringSetAsync() => this function take the Value as a JsonFile so we Serilezed it first
            var CreatedOrUdpated = database.StringSetAsync(Basket.Id, JsonBasket , TimeSpan.FromDays(1));
            if (CreatedOrUdpated == null) return null;
            else
                return await GetBasketAsync(Basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await database.KeyDeleteAsync(BasketId);
        }

    }
}
