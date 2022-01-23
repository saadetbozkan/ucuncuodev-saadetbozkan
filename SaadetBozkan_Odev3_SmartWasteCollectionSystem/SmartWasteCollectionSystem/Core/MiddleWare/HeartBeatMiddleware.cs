using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace Core.MiddleWare
{
    public class HeartBeatMiddleware
    {
        private readonly RequestDelegate next;

        public HeartBeatMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/heartbeat"))
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("Hello From Server");
                return;
            }

            await next.Invoke(context);
        }
    }
}
