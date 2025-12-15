using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.Authentication.Command.Models;

namespace ThyroCareX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost("register-doctor")]
        public async Task<IActionResult> RegisterDoctor([FromForm] RegisterDoctorCommand cmd)
        {
            var response = await Mediator.Send(cmd);
            return Ok(response);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand cmd)
        {
            var response = await Mediator.Send(cmd);
            return Ok(response);
        }

    }
}
