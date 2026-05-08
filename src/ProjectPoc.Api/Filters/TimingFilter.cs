using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ProjectPoc.Api.Filters
{
    /// <summary>
    /// Resource Filters – Run after authorization but before model binding; useful for caching or timing
    /// </summary>
    public class TimingFilter : IResourceFilter
    {
        private Stopwatch _sw;
        
        public void OnResourceExecuting(ResourceExecutingContext ctx) => _sw = Stopwatch.StartNew();
        
        public void OnResourceExecuted(ResourceExecutedContext ctx)
        {
            _sw.Stop();
            Console.WriteLine($"Took {_sw.ElapsedMilliseconds} ms");
        }
    }
}
