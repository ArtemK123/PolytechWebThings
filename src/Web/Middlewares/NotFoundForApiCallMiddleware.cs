using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.Middlewares
{
    internal class NotFoundForApiCallMiddleware
    {
        private readonly RequestDelegate next;

        public NotFoundForApiCallMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (IsApiCall(context))
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            await next(context);
        }

        private bool IsApiCall(HttpContext httpContext) => httpContext.Request.Path.Value?.Contains("/api") ?? false;
    }
}