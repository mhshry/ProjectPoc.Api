namespace ProjectPoc.Api.Extensions
{
    public static class CorrelationMiddlewareExtensions
    {
        static public IApplicationBuilder UseCorrelationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middlewares.CorrelationMiddleware>();
        }
    }
}
