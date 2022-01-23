using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Core.MiddleWare
{
    public class GetByIdMiddleWare
    {
        private readonly RequestDelegate next;

        public GetByIdMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context) 
        {
            if(context.Request.Method == "GET" && context.Request.Path.StartsWithSegments("/api/Vehicle/id"))
            {
                context.Response.StatusCode = 403;  
                return;
            }
            
            await next.Invoke(context);       
        }
    }
}
