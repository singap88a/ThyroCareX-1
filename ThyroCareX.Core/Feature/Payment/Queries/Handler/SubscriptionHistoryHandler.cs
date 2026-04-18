using MediatR;
using Microsoft.EntityFrameworkCore;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Payment.Queries.Models;
using ThyroCareX.Data.Enums;
using ThyroCareX.Infrastructure.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Feature.Payment.Queries.Handler
{
    public class SubscriptionHistoryHandler(ISubscriptionPlanRepo _subscriptionPlanRepo) : ResponseHandler, 
        IRequestHandler<GetSubscriptionHistoryQuery, Response<List<SubscriptionHistoryDto>>>
    {
        public async Task<Response<List<SubscriptionHistoryDto>>> Handle(GetSubscriptionHistoryQuery request, CancellationToken cancellationToken)
        {
            var history = await _subscriptionPlanRepo.GetTableNoTracking()
                .Include(x => x.Doctor)
                .Include(x => x.Plan)
                .OrderByDescending(x => x.SubscriptionPlanID)
                .Select(x => new SubscriptionHistoryDto
                {
                    Id = x.SubscriptionPlanID,
                    DoctorName = x.Doctor != null ? x.Doctor.FullName : "Unknown",
                    DoctorEmail = x.Doctor != null ? x.Doctor.Email : "",
                    PlanType = (int)x.Plan.PlanType,
                    PlanPrice = x.Plan.Price,
                    Status = (int)x.Status,
                    StartDate = x.StartDate,
                    TransactionId = x.TransactionId,
                    OrderId = x.OrderId,
                    Features = x.Plan != null ? x.Plan.Features : new List<string>(),
                    DurationInDays = x.Plan != null ? x.Plan.DurationInDays : 30
                })
                .ToListAsync(cancellationToken);

            return Success(history);
        }
    }
}
