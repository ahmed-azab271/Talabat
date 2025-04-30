using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.IServices;

namespace TalabatAPIs.Helpers
{
    public class CacheAttribute : Attribute , IAsyncActionFilter
    {
        private readonly int expiredTimeInSeconds;

        public CacheAttribute(int _ExpiredTimeInSeconds)
        {
            expiredTimeInSeconds = _ExpiredTimeInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
 //There Are to ways to ask CLR to inject an object 1=> (Implicitly) throw CTOR 2=> (Explicitly) Like what we wil do  
            var CachedService = context.HttpContext.RequestServices.GetRequiredService<ICachedService>();

            var CacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var CachedResponce = await CachedService.GetCahedAsync(CacheKey);

            if(!string.IsNullOrEmpty(CachedResponce))
            {
                var contentResult = new ContentResult()
                {
                    Content = CachedResponce,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                context.Result = contentResult;
                return;
            }
            var ExecutedEndPoint = await next.Invoke(); // Will Excute the EndPoint

            if(ExecutedEndPoint.Result is OkObjectResult result) // If the Excutions is Ok
                await CachedService.CachingAsync(CacheKey, result.Value , TimeSpan.FromSeconds(expiredTimeInSeconds));
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path); // api/{controller} 

            foreach(var (key , value) in request.Query.OrderBy(X=>X.Key))
                keyBuilder.Append($"|{key}-{value}");
            // api/Products|PageIndex-1|PageSize-5|Sort-Name

            return keyBuilder.ToString();
        }
    }
}
