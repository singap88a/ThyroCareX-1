using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.TestWithAI.Commands.Models;
using ThyroCareX.Data.Healpers.ClinicalAI;

namespace ThyroCareX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class TestsWithAIController : AppControllerBase
    {
    

        [HttpPost("ProcessImage")]
        public async Task<IActionResult> ProcessImage([FromForm] PredictImageCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("ProcessClinical")]
        public async Task<IActionResult> ProcessClinical([FromBody] ClinicalRequest request)
        {
            var response = await Mediator.Send(new AssessClinicalCommand(request));
            return Ok(response);
        }

        [HttpPost("ProcessFnac")]
        public async Task<IActionResult> ProcessFnac([FromForm]PredictFnacCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
