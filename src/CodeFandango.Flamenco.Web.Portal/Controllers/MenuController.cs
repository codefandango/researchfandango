using Microsoft.AspNetCore.Mvc;

namespace CodeFandango.Flamenco.Web.Portal.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Admin() => PartialView();

        public IActionResult Participation() => PartialView();
    }
}
