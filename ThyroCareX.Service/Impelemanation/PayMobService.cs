using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using ThyroCareX.Data.Enums;
using ThyroCareX.Data.Healpers;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Service.Impelemanation
{
    public class PayMobService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly PayMobSettings _payMobSettings;
        private readonly IPlanRepo _planRepo;
        private readonly IDoctorRepository _doctorRepo;
        private readonly ISubscriptionPlanRepo _subscriptionPlanRepo;

        public PayMobService(HttpClient httpClient, 
                             IOptions<PayMobSettings> payMobSettings,
                             IPlanRepo planRepo,
                             IDoctorRepository doctorRepo,
                             ISubscriptionPlanRepo subscriptionPlanRepo)
        {
            _httpClient = httpClient;
            _payMobSettings = payMobSettings.Value;
            _planRepo = planRepo;
            _doctorRepo = doctorRepo;
            _subscriptionPlanRepo = subscriptionPlanRepo;
        }

        public async Task<string> CreatePayment(int planId, int doctorId)
        {
            // 1. Get Plan and Doctor details
            var plan = await _planRepo.GetByIdAsync(planId);
            if (plan == null) throw new Exception("Plan not found");

            var doctor = await _doctorRepo.GetByIdAsync(doctorId);
            if (doctor == null) throw new Exception("Doctor not found");

            long amountCents = (long)(plan.Price * 100);

            // 2. Step 1: Authentication
            var authToken = await GetAuthToken();

            // 3. Step 2: Order Registration
            var orderId = await RegisterOrder(authToken, amountCents);

            // 4. Step 3: Payment Key Generation
            var paymentToken = await GetPaymentKey(authToken, amountCents, orderId, doctor);

            // 5. Create Pending Subscription record
            var subscription = new SubscriptionPlan
            {
                PlanId = planId,
                DoctorId = doctorId,
                Status = Data.Enums.SubscriptionStatus.Pending, // Start as Pending
                OrderId = orderId.ToString()
            };
            await _subscriptionPlanRepo.AddAsync(subscription);

            // 6. Return Iframe URL
            return $"https://accept.paymob.com/api/acceptance/iframes/{_payMobSettings.IframeId}?payment_token={paymentToken}";
        }

        private async Task<string> GetAuthToken()
        {
            var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/auth/tokens", new PayMobAuthRequest
            {
                ApiKey = _payMobSettings.ApiKey
            });

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PayMobAuthResponse>();
            return result?.Token ?? throw new Exception("Failed to get PayMob auth token");
        }

        private async Task<long> RegisterOrder(string authToken, long amountCents)
        {
            var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/ecommerce/orders", new PayMobOrderRequest
            {
                AuthToken = authToken,
                AmountCents = amountCents,
                Currency = "EGP",
                DeliveryNeeded = "false",
                Items = new List<object>()
            });

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PayMobOrderResponse>();
            return result?.Id ?? throw new Exception("Failed to register PayMob order");
        }

        private async Task<string> GetPaymentKey(string authToken, long amountCents, long orderId, Data.Models.Doctor doctor)
        {
            var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/acceptance/payment_keys", new PayMobPaymentKeyRequest
            {
                AuthToken = authToken,
                AmountCents = amountCents,
                Expiration = 3600,
                OrderId = orderId.ToString(),
                Currency = "EGP",
                IntegrationId = int.Parse(_payMobSettings.CardIntegrationId),
                NotificationUrl = _payMobSettings.NotificationUrl,
                BillingData = new BillingData
                {
                    FirstName = doctor.FullName ?? "NA",
                    LastName = "NA",
                    Email = doctor.Email ?? "NA",
                    PhoneNumber = doctor.PhoneNumber ?? "NA",
                    City = "NA",
                    Country = "Egypt",
                    State = "NA",
                    Street = "NA",
                    Building = "NA",
                    Floor = "NA",
                    Apartment = "NA"
                }
            });

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PayMobPaymentKeyResponse>();
            return result?.Token ?? throw new Exception("Failed to get PayMob payment key");
        }

        public bool VerifyHmac(IDictionary<string, string> queryParams)
        {
            // PayMob HMAC validation logic (for Step 4: Transaction Callback)
            // It concatenates specific fields in a specific order and hashes them with HMAC-SHA512 using the secret
            
            if (!queryParams.TryGetValue("hmac", out var receivedHmac)) return false;

            var keys = new[] { "amount_cents", "created_at", "currency", "error_occured", "has_parent_transaction", "id", "integration_id", "is_3d_secure", "is_auth", "is_capture", "is_refunded", "is_standalone_payment", "is_voided", "order", "owner", "pending", "source_data.pan", "source_data.sub_type", "source_data.type", "success" };
            
            var sb = new StringBuilder();
            foreach (var key in keys)
            {
                if (queryParams.TryGetValue(key, out var value))
                {
                    sb.Append(value);
                }
            }

            var secretKeyBytes = Encoding.UTF8.GetBytes(_payMobSettings.HmacSecret);
            var messageBytes = Encoding.UTF8.GetBytes(sb.ToString());

            using var hmac = new HMACSHA512(secretKeyBytes);
            var hashBytes = hmac.ComputeHash(messageBytes);
            var calculatedHmac = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return calculatedHmac == receivedHmac.ToLower();
        }

        public (bool isValid, string concatenatedString) VerifyHmac(System.Text.Json.JsonElement body, string? hmacOverride = null)
        {
            var receivedHmac = hmacOverride;
            
            if (string.IsNullOrEmpty(receivedHmac))
            {
                if (!body.TryGetProperty("hmac", out var hmacProp)) return (false, "MISSING_HMAC");
                receivedHmac = hmacProp.GetString();
            }

            if (!body.TryGetProperty("obj", out var obj)) return (false, "MISSING_OBJ");

            var sb = new StringBuilder();
            var keys = new[] { "amount_cents", "created_at", "currency", "error_occured", "has_parent_transaction", "id", "integration_id", "is_3d_secure", "is_auth", "is_capture", "is_refunded", "is_standalone_payment", "is_voided", "order", "owner", "pending", "source_data.pan", "source_data.sub_type", "source_data.type", "success" };

            foreach (var key in keys)
            {
                string value = string.Empty;
                if (key.Contains("."))
                {
                    var parts = key.Split('.');
                    if (obj.TryGetProperty(parts[0], out var parent) && parent.ValueKind != System.Text.Json.JsonValueKind.Null)
                    {
                        if (parent.TryGetProperty(parts[1], out var child))
                        {
                            value = GetJsonStringValue(child);
                        }
                    }
                }
                else if (key == "order")
                {
                    if (obj.TryGetProperty("order", out var order) && order.TryGetProperty("id", out var orderId))
                    {
                        value = orderId.ToString();
                    }
                }
                else
                {
                    if (obj.TryGetProperty(key, out var prop))
                    {
                        value = GetJsonStringValue(prop);
                    }
                }
                sb.Append(value);
            }

            var concatenatedString = sb.ToString();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_payMobSettings.HmacSecret);
            var messageBytes = Encoding.UTF8.GetBytes(concatenatedString);

            using var hmac = new HMACSHA512(secretKeyBytes);
            var hashBytes = hmac.ComputeHash(messageBytes);
            var calculatedHmac = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return (calculatedHmac == (receivedHmac?.ToLower() ?? string.Empty), concatenatedString);
        }

        private string GetJsonStringValue(System.Text.Json.JsonElement element)
        {
            return element.ValueKind switch
            {
                System.Text.Json.JsonValueKind.True => "true",
                System.Text.Json.JsonValueKind.False => "false",
                System.Text.Json.JsonValueKind.Null => "",
                _ => element.ToString()
            };
        }

        public async Task UpdateSubscriptionStatus(string orderId, bool success, string transactionId)
        {
            var subscription = await _subscriptionPlanRepo
                .GetTableAsTracking()
                .FirstOrDefaultAsync(x => x.OrderId == orderId);

            if (subscription == null)
                return;

            // 🔥 update real transaction id
            subscription.TransactionId = transactionId;

            if (success)
            {
                var plan = await _planRepo.GetByIdAsync(subscription.PlanId);

         

                subscription.Status = SubscriptionStatus.Active;
                subscription.StartDate = DateTime.UtcNow;
                subscription.EndDate = DateTime.UtcNow.AddDays(plan.DurationInDays);
            }
            else
            {
                subscription.Status = SubscriptionStatus.Failed;
            }
            await _subscriptionPlanRepo.UpdateAsync(subscription);

        }
    }
    }


