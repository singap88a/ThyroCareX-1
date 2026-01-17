using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Service.Impelemanation
{
    public class WebhookLogService : IWebhookLogService
    {
        #region prop
        private readonly IWebhookLogRepo _webhookLogRepo;
        #endregion

        #region Constructor
        public WebhookLogService(IWebhookLogRepo webhookLogRepo)
        {
            _webhookLogRepo = webhookLogRepo;
        }
        #endregion

        #region Handle Function
        public async Task LogAsync(Event stripeEvent)
        {
            var exists = await _webhookLogRepo.GetTableNoTracking()
          .AnyAsync(x => x.StripeEventId == stripeEvent.Id);

            if (exists)
                return;

            await _webhookLogRepo.AddAsync(new WebhookLog
            {
                StripeEventId = stripeEvent.Id,
                EventType = stripeEvent.Type,
                Payload = stripeEvent.ToString(),
                CreatedAt = DateTime.UtcNow
            });

            await _webhookLogRepo.SaveChangeAsync();
        }

        public async Task<bool> IsProcessedAsync(string stripeEventId)
        {
            var log = await _webhookLogRepo.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.StripeEventId == stripeEventId);

            return log != null && log.ProcessedAt != null;
        }


        public async Task MarkAsProcessedAsync(string stripeEventId)
        {
            var log = await _webhookLogRepo.GetTableAsTracking()
                .FirstOrDefaultAsync(x => x.StripeEventId == stripeEventId);

            if (log != null)
            {
                log.ProcessedAt = DateTime.UtcNow;
                await _webhookLogRepo.SaveChangeAsync();
            }
        }
        #endregion
    }
}
