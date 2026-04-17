using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace ThyroCareX.Service.Abstarct
{
    public interface IPaymentService
    {
        Task<string> CreatePayment(int planId, int doctorId);
        bool VerifyHmac(IDictionary<string, string> queryParams);
        (bool isValid, string concatenatedString) VerifyHmac(System.Text.Json.JsonElement body, string? hmacOverride = null);
        Task UpdateSubscriptionStatus(string orderId, bool success, string transactionId);
    }
}

