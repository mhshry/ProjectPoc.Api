using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectPoc.Api.Filters
{
    /// <summary>
    /// Action Filters – Surround action execution; can modify inputs or results
    /// </summary>
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext ctx) =>
            Console.WriteLine($"Args: {string.Join(", ", ctx.ActionArguments)}");

        public void OnActionExecuted(ActionExecutedContext ctx) =>
            Console.WriteLine("Action completed");
    }
}
