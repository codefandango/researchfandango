using CodeFandango.Flamenco.Web.Portal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFandango.Flamenco.Web.Portal.Controllers.API
{
    [Authorize]
    [Route("api")]
    public class ApiController : FlamencoApiController
    {
        private readonly IUniqueCodeGenerator uniqueCodeGenerator;

        public ApiController(IUniqueCodeGenerator uniqueCodeGenerator)
        {
            this.uniqueCodeGenerator = uniqueCodeGenerator;
        }

        [HttpGet("code/{scope}")]
        public async Task<IActionResult> GetUniqueCode(string scope, string value) 
            => SuccessResponse(await uniqueCodeGenerator.GenerateUniqueCode(scope, value));
    }
}
