using MediatR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Payment.Commands.Models;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.Payment.Commands.Handler
{
    public class PaymobCallbackCommandHandler : ResponseHandler, IRequestHandler<PaymobCallbackCommand>
       
    {
        private readonly IPaymentService _paymentService;

        public PaymobCallbackCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(PaymobCallbackCommand request, CancellationToken cancellationToken)
        {
            if (!request.Body.TryGetProperty("obj", out var obj))
                return;

            // PayMob IDs can be integers in JSON, Use ToString() for safety
            var orderId = obj
                .GetProperty("order")
                .GetProperty("id")
                .ToString();

            var transactionId = obj
                .GetProperty("id")
                .ToString();

            var success = obj.GetProperty("success").GetBoolean();

            // 1. Verify HMAC for security
            var hmacResult = _paymentService.VerifyHmac(request.Body, request.Hmac);
            if (!hmacResult.isValid)
            {
                // 🔥 Log the concatenated string into the TransactionId field so we can see EXACTLY what was hashed
                await _paymentService.UpdateSubscriptionStatus(orderId, false, $"FAIL_HMAC: {hmacResult.concatenatedString}");
                return;
            }

            await _paymentService.UpdateSubscriptionStatus(orderId, success, transactionId);
        }
    }
}
