using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFandango.Flamenco.Web.Portal.Controllers
{
    [Authorize]
    [Route("admin")]
    public class AdminController : Controller
    {

        [Route("studies")]
        public IActionResult Studies()
        {
            return View();
        }

        [Route("surveys")]
        public IActionResult Surveys()
        {
            return View();
        }
    }
}
