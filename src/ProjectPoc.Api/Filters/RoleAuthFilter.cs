using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectPoc.Api.Filters
{
    /// <summary>
    ///  Run first, check if a user is authorized, and can short-circuit requests.
    /// </summary>
    public class RoleAuthFilter : IAuthorizationFilter
    {
        private readonly string _role;

        public RoleAuthFilter(string role) => _role = role;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.IsInRole(_role))
                context.Result = new ForbidResult();
        }
    }
}
