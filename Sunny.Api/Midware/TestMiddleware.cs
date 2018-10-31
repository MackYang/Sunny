using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Sunny.Common.ConfigOption;
using System.Threading.Tasks;

namespace Sunny.Api.Midware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IDistributedCache cache;
        private readonly TokenValidateOption option;


        public TestMiddleware(RequestDelegate next, IDistributedCache cache,IOptions<TokenValidateOption> options)
        {
            this.next = next;
            this.cache = cache;
            this.option = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            bool validOk = false;
            StringValues token = StringValues.Empty;

            if (context.Request.Headers.TryGetValue(option.TokenKey, out token))
            {
                if (await cache.ExistsAsync(token))
                {
                    validOk = true;
                }
            }
            if (!validOk)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                await next(context);
            }
        }

    }

}
