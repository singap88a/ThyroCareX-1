using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ThyroCareX.Data.Healpers
{
    public class StripeSettings
    {
        public string SecretKey { get; set; }           // المفتاح السري للتعامل مع Stripe API
        public string WebhookSecret { get; set; }       // المفتاح للتحقق من Webhook Events
        public string[] WebhookHosts { get; set; }      // قائمة الـ hosts المسموح لها بالوصول للـ webhook
    }
}
