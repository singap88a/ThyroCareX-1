using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.AplicationUser.Command.Models;

namespace ThyroCareX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            var response= await Mediator.Send(command);
            return Ok(response);
        }
    }
}
