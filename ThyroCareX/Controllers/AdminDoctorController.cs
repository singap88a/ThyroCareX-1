using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.Doctors.Commands.Models;
using ThyroCareX.Core.Feature.Doctors.Queires.Models;

namespace ThyroCareX.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDoctorController : AppControllerBase
    {
        
        
        [HttpGet("DisplayAllDoctors")]
        public async Task<IActionResult> DisplayAllDoctors()
        {
            var Response = await Mediator.Send(new GetDoctorListQuery());
            return Ok(Response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleDoctorById([FromRoute]int id) 
        {
            var Response=await Mediator.Send(new GetDoctorByIdQuery(id));
            return Ok(Response);
        }
        
        
        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<IActionResult> DeleteDoctor([FromRoute] int id)
        {
            var Response = await Mediator.Send(new DeleteDoctorCommand(id));
            return Ok(Response);
        }

        [HttpGet("Pending")]
        public async Task<IActionResult> GetPendingDoctors()
        {
            var response = await Mediator.Send(new GetPendingDoctorsQuery());
            return Ok(response);
        }

        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> ApproveDoctor([FromRoute] int id)
        {
            var response = await Mediator.Send(new ApproveDoctorCommand { Id = id });
            return Ok(response);
        }

        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> RejectDoctor([FromRoute] int id)
        {
            var response = await Mediator.Send(new RejectDoctorCommand { Id = id });
            return Ok(response);
        }
    }
}
