using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.Payment.Commands.Models;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : AppControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment(CreatePaymentCommandRequest command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);

        }

        [HttpGet("return")]
        public IActionResult Return([FromQuery] Dictionary<string, string> query)
        {
            var success = query.ContainsKey("success") && query["success"] == "true";

            return success
                ? Redirect("https://frontend.com/payment-success")
                : Redirect("https://frontend.com/payment-failed");
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook([FromBody] JsonElement body, [FromQuery] string hmac)
        {
            await Mediator.Send(new PaymobCallbackCommand
            {
                Body = body,
                Hmac = hmac
            });

            return Ok();
        }

    }
}
