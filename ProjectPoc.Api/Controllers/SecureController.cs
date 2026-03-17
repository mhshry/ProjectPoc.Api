using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectPoc.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecureController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult Public() => Ok("This is public");

        [HttpGet("user")]
        [Authorize]
        public IActionResult UserEndpoint() => Ok($"Hello {User.Identity?.Name}. You are authenticated.");

        [HttpGet("role-admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndpoint() => Ok("Hello Admin. You have the Admin role.");

        [HttpGet("policy-department")]
        [Authorize(Policy = "HasDepartment")]
        public IActionResult DepartmentEndpoint() => Ok($"Department claim present: {User.FindFirst("department")?.Value}");
    }
}
