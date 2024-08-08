using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFandango.Flamenco.Web.Portal.Controllers
{
    [Authorize]
    [Route("admin")]
    public class AdminController : Controller
    {

        [Route("studies")]
        public IActionResult Studies() => View();

        [Route("surveys")]
        public IActionResult Surveys() => View();

        [Route("customers")]
        public IActionResult Customers() => View();

        [Route("participation")]
        public IActionResult Participation(long studyId = 0, long surveyId = 0)
        {
            if (surveyId == 0 && studyId == 0)
            {
                return NotFound();
            }

            if (surveyId != 0)
            {
                return View(model: ("survey", surveyId));
            }
            else
            {
                return View(model: ("study", studyId));
            }
        }

    }
}
