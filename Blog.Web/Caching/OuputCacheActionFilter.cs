using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blog.Web.Caching
{
    public class OuputCacheActionFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cache = context.HttpContext.RequestServices.GetService<IMemoryCache>();

            var requestFeature = context.HttpContext.Features.Get<IHttpRequestFeature>();
            var url = requestFeature.RawTarget;

            ActionExecutedContext result = null;
            if (!cache.TryGetValue(url, out result))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(10))
                    .SetSlidingExpiration(TimeSpan.FromDays(3));

                result = await next();

                cache.Set(url, result, cacheEntryOptions);
            }
            else
            {
                context.HttpContext.Response.Headers.Add("cache-origin", "memory");
            }

            var nextResult = await cache.GetOrCreateAsync(url, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(10);
                entry.SlidingExpiration = TimeSpan.FromDays(3);

                return await next();
            });

            context.Result = nextResult.Result;
        }
    }
}
