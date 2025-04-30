using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.IServices
{
    public interface ICachedService
    {
        Task CachingAsync(string CacheKey, object Responce, TimeSpan ExpiredTime);
        Task<string?> GetCahedAsync(string CacheKey);
    }
}
