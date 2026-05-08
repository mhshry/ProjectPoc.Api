using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectPoc.Api.Filters
{
    /// <summary>
    /// Exception Filters – Handle unhandled exceptions in actions/results.
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext ctx)
        {
            ctx.Result = new ObjectResult(new { Error = "Unexpected error" }) { StatusCode = 500 };
            ctx.ExceptionHandled = true;
        }
    }
}
