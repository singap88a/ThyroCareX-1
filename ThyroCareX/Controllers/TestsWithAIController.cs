using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.TestWithAI.Commands.Models;

namespace ThyroCareX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class TestsWithAIController : AppControllerBase
    {
        [HttpPost("AddTest")]
        public async Task<IActionResult> AddTest([FromForm] AddTestCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("ProcessImage/{testId}")]
        public async Task<IActionResult> ProcessImage(int testId)
        {
            var response = await Mediator.Send(new PredictImageCommand { TestId = testId });
            return Ok(response);
        }

        [HttpPost("ProcessClinical/{testId}")]
        public async Task<IActionResult> ProcessClinical(int testId)
        {
            var response = await Mediator.Send(new AssessClinicalCommand { TestId = testId });
            return Ok(response);
        }

        [HttpPost("ProcessFnac/{testId}")]
        public async Task<IActionResult> ProcessFnac(int testId)
        {
            var response = await Mediator.Send(new PredictFnacCommand { TestId = testId });
            return Ok(response);
        }
    }
}
