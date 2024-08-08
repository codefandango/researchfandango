using CodeFandango.Flamenco.Models.Participation;
using CodeFandango.Flamenco.Web.Portal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFandango.Flamenco.Web.Portal.Controllers.API
{
    [Authorize]
    [Route("api/participation")]
    public class ParticipationApiController : FlamencoApiController
    {

        private readonly IParticipationService participationService;

        public ParticipationApiController(IParticipationService participationService)
        {
            this.participationService = participationService;
        }

        [Route("setup/{type}/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetParticipationSetup(string type, long id) => 
            SuccessResponse(await participationService.GetParticipationSetup(type, id));

        [Route("field")]
        [HttpPost]
        public async Task<IActionResult> CreateField([FromBody] ParticipantFieldDefinitionModel field) => 
            SuccessResponse(await participationService.UpdateOrCreateField(field));

    }
}
