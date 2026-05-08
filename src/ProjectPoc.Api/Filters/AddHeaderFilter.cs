using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectPoc.Api.Filters
{
    /// <summary>
    /// Result Filters – Run before/after the result is executed; ideal for modifying responses.
    /// Prefer middleware for global concerns; filters for MVC-specific logic.
    /// </summary>
    public class AddHeaderFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext ctx) =>
            ctx.HttpContext.Response.Headers.Add("X-App", "POC");

        public void OnResultExecuted(ResultExecutedContext ctx) { }
    }
}
