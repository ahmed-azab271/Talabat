using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.IServices;

namespace Talabat.Services
{
    public class CachedService : ICachedService
    {
        private readonly IDatabase database;
        public CachedService(IConnectionMultiplexer Redis)
        {
            database = Redis.GetDatabase();
        }
        public async Task CachingAsync(string CacheKey, object Responce, TimeSpan ExpiredTime)
        {
            if (Responce == null) return;
            var Options = new JsonSerializerOptions()
            {
                // To Return any Prop to JSON in CamelCase option (FirstName) => (firstName) 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            };
            var SerilizedResponce = JsonSerializer.Serialize(Responce , Options);
            await database.StringSetAsync(CacheKey , SerilizedResponce, ExpiredTime);
        }

        public async Task<string?> GetCahedAsync(string CacheKey)
        {
            var Responce =  await database.StringGetAsync(CacheKey);
            if (Responce.IsNullOrEmpty) return null;
            return Responce;
        }
    }
}
