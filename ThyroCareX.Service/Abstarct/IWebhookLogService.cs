using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Service.Abstarct
{
    public interface IWebhookLogService
    {
        Task LogAsync(Event stripeEvent);
        Task<bool> IsProcessedAsync(string stripeEventId);
        Task MarkAsProcessedAsync(string stripeEventId);
    }
}
