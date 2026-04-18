using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.Contact.Commands.Model;
using ThyroCareX.Core.Feature.Contact.Queries.Model;

namespace ThyroCareX.Api.Controllers
{
    [ApiController]

    public class ContactController : AppControllerBase
    {
        [HttpPost("/api/Contact/Submit")]
        [AllowAnonymous]
        public async Task<IActionResult> Submit([FromForm] SubmitContactMessageCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("/api/Contact/List")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetList()
        {
            var response = await Mediator.Send(new GetContactMessagesQuery());
            return Ok(response);
        }

        [HttpPut("/api/Contact/ToggleStatus/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var response = await Mediator.Send(new ToggleRepliedStatusCommand { Id = id });
            return Ok(response);
        }

        [HttpDelete("/api/Contact/Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new DeleteContactCommand { Id = id });
            return Ok(response);
        }
    }
}
