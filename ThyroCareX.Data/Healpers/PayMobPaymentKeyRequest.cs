using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThyroCareX.Data.Healpers
{
    public class PayMobPaymentKeyRequest
    {
        [JsonPropertyName("auth_token")]
        public string AuthToken { get; set; } = string.Empty;

        [JsonPropertyName("amount_cents")]
        public long AmountCents { get; set; }

        [JsonPropertyName("expiration")]
        public int Expiration { get; set; } = 3600;

        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;

        [JsonPropertyName("billing_data")]
        public BillingData BillingData { get; set; } = new();

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = "EGP";

        [JsonPropertyName("integration_id")]
        public int IntegrationId { get; set; }

        [JsonPropertyName("lock_order_when_paid")]
        public string LockOrderWhenPaid { get; set; } = "false";
        
        [JsonPropertyName("redirection_url")]
        public string RedirectionUrl { get; set; } = string.Empty;

        public string NotificationUrl { get; set; }
    }
}
