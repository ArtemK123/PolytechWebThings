using Microsoft.AspNetCore.Builder;

namespace Web.Middlewares
{
    public static class NotFoundForApiCallMiddlewareExtensions
    {
        public static IApplicationBuilder UseNotFoundForApiCall(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NotFoundForApiCallMiddleware>();
        }
    }
}