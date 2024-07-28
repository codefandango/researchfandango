using CodeFandango.Flamenco.DataAccess;
using CodeFandango.Flamenco.Models.DataEntry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFandango.Flamenco.Web.Portal.Controllers.API
{
    [Authorize]
    [Route("api/client")]
    public class ClientApiController : FlamencoApiController
    {
        private readonly IDataAccess data;

        public ClientApiController(IDataAccess data)
        {
            this.data = data;
        }

        [HttpGet("fields")]
        public IActionResult GetFields() => SuccessResponse(data.Clients.GetEditableFields());

        [HttpPost("fields")]
        public IActionResult SaveFields([FromBody] Dictionary<string, string> fields) => SuccessResponse(data.Clients.SaveFieldValues(fields));

    }
}
