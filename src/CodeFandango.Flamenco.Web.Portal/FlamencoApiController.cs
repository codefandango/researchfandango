using CodeFandango.Flamenco.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFandango.Flamenco.Web.Portal
{
    public class FlamencoApiController : ControllerBase
    {

        public IActionResult SuccessResponse(Success success)
        {
            if (success.Successful)
            {
                return Ok(success);
            }
            else
            {
                if (success.HttpStatusCode > 0)
                {
                    return StatusCode(success.HttpStatusCode, success.StatusMessage);
                }
                else
                {
                    return BadRequest(success);
                }
            }
        }

        public IActionResult SuccessResponse<T>(Success<T> success)
        {
            if (success.Successful)
            {
                return Ok(success);
            }
            else
            {
                if (success.HttpStatusCode > 0)
                {
                    return StatusCode(success.HttpStatusCode, success.Model);
                }
                else
                {
                    return BadRequest(success);
                }
            }
        }

    }
}