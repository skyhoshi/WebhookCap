using Microsoft.AspNetCore.Builder;

namespace Catcher_WebAPI.Middleware
{
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestDatabaseLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLogger>();
        }
    }
}