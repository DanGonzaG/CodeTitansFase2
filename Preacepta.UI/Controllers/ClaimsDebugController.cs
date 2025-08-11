using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Preacepta.UI.Controllers
{
    [Authorize]
    [Route("debug/claims")]
    public class ClaimsDebugController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var claims = User.Claims.Select(c => new {
                Type = c.Type,
                Value = c.Value
            });

            return Json(new
            {
                User.Identity.Name,
                Claims = claims
            });
        }
    }

}
