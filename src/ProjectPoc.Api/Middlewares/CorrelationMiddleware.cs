namespace ProjectPoc.Api.Middlewares
{
    public sealed class CorrelationMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationMiddleware(RequestDelegate next) => _next = next;
        
        public async Task InvokeAsync(HttpContext context)
        {
            // Check for existing correlation ID in headers
            if (!context.Request.Headers.TryGetValue("X-Correlation-ID", out var correlationId))
            {
                // Generate new correlation ID if not present
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers["X-Correlation-ID"] = correlationId;
            }
            // Add correlation ID to response headers for client visibility
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Correlation-ID"] = correlationId;
                return Task.CompletedTask;
            });
            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
