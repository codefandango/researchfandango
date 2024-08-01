using CodeFandango.Flamenco.DataAccess;
using CodeFandango.Flamenco.Models.Surveys;
using CodeFandango.Flamenco.Web.Portal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFandango.Flamenco.Web.Portal.Controllers.API
{
    [Authorize]
    [Route("api/surveys")]
    public class SurveysApiController : FlamencoApiController
    {
        private readonly ISurveyService surveys;

        public SurveysApiController(ISurveyService surveys)
        {
            this.surveys = surveys;
        }

        [HttpGet]
        public async Task<IActionResult> GetSurveys() => SuccessResponse(await surveys.GetObjects());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSurvey(int id) => SuccessResponse(await surveys.GetObject(id));

        [HttpGet("definition")]
        public async Task<IActionResult> GetSurveyDefinition() => SuccessResponse(await surveys.GetDefinition());

        [HttpPost]
        public async Task<IActionResult> CreateSurvey([FromBody] SurveyModel survey) => SuccessResponse(await surveys.CreateOrUpdateObject(survey));

    }
}
