using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.Doctors.Commands.Models;
using ThyroCareX.Core.Feature.Doctors.Queires.Models;

namespace ThyroCareX.Controllers
{
    [Authorize(Roles = "Doctor")]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorProfileController : AppControllerBase
    {
        
        [HttpGet("profile")]
        public async Task<IActionResult> GetDoctorProfile()
        {
            var response = await Mediator.Send(new GetDoctorProfileQuery());
            return Ok(response);
        }

        [HttpPut("Updateprofile")]
        public async Task<IActionResult> UpdateDoctorProfile([FromForm] EditDoctorCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
