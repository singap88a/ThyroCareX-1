using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.Community.Queries.Models;
using ThyroCareX.Core.Feature.Plans.Commands.Model;
using ThyroCareX.Core.Feature.Plans.Queries.Models;

namespace ThyroCareX.Controllers
{

    public class PlanController : AppControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> AllPlans()
        {
            var response = await Mediator.Send(new PlanListQuery());
            if (response == null)
                return BadRequest("No Plan Now");

            return Ok(response);


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanById(int id)
        {
            var response = await Mediator.Send(new SinglePlanQuery(id));
            if (response == null)
                return BadRequest("No Plan Now");
            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> AddPlan([FromBody]AddPlanCommand command)
        {
            var response = await Mediator.Send(command);
            if (response == null)
                return BadRequest("Failed To Add Plan");
            return Ok(response);
        }
    }
}
