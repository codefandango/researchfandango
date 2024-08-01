using CodeFandango.Flamenco.DataAccess;
using CodeFandango.Flamenco.Models.Studies;
using CodeFandango.Flamenco.Web.Portal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFandango.Flamenco.Web.Portal.Controllers.API
{
    [Authorize]
    [Route("api/studies")]
    public class StudiesApiController : FlamencoApiController
    {

        private readonly IStudyService studies;

        public StudiesApiController(IStudyService studies)
        {
            this.studies = studies;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudies() => SuccessResponse(await studies.GetObjects());

        [HttpGet("definition")]
        public async Task<IActionResult> GetStudyDefinition() => SuccessResponse(await studies.GetDefinition());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudy(int id) => SuccessResponse(await studies.GetObject(id));

        [HttpPost]
        public async Task<IActionResult> CreateStudy([FromBody] StudyModel study) => SuccessResponse(await studies.CreateOrUpdateStudy(study));

    }
}
