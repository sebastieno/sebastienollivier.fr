using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Web.Filters
{
    public class OuputCacheActionFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cache = context.HttpContext.RequestServices.GetService<IMemoryCache>();

            var requestFeature = context.HttpContext.Features.Get<IHttpRequestFeature>();
            var url = requestFeature.RawTarget;

            var nextResult = await cache.GetOrCreateAsync(url, async entry =>
            {
                return await next();
            });

            context.Result = nextResult.Result;
        }
    }
}
