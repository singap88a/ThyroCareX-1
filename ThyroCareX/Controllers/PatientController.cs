using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.Patients.Command.Model;
using ThyroCareX.Core.Feature.Patients.Queries.Models;

namespace ThyroCareX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientController : AppControllerBase
    {
        [HttpGet("DisplayAllPatients")]
        public async Task<IActionResult> DisplayAllPatients()
        {
            var Response = await Mediator.Send(new GetPatientListQuery());
            return Ok(Response);
        }
        [HttpGet("DisplayPatientsByDoctor/{doctorId}")]
        public async Task<IActionResult> DisplayPatientsByDoctor([FromRoute] int doctorId)
        {
            var Response = await Mediator.Send(new GetPatientListQueryByDoctor (doctorId));
            return Ok(Response);
        }
        [HttpGet("DisplayPatientById/{id}")]
        public async Task<IActionResult> DisplayPatientById([FromRoute] int id)
        {
            var Response = await Mediator.Send(new GetPatientByIdQuery(id));
            return Ok(Response);
        }
        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatient([FromBody]AddPatientCommand command)
        {
            var Response = await Mediator.Send(command);
            return Ok(Response);
        }
        [HttpPut("EditPatient/{Id}")]
        public async Task<IActionResult> EditPatient([FromRoute] int Id, [FromBody] EditPatientCommand command)
        {
            command.PatientID = Id;
            var Response = await Mediator.Send(command);
            return Ok(Response);

        }
        [HttpDelete("DeletePatient/{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeletePatientCommand(id));
            return Ok(response);
        }
    }
}
