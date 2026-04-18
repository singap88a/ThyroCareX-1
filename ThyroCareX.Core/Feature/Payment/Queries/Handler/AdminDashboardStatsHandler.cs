using MediatR;
using Microsoft.EntityFrameworkCore;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Payment.Queries.Models;
using ThyroCareX.Data.Enums;
using ThyroCareX.Infrastructure.Abstarct;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Feature.Payment.Queries.Handler
{
    public class AdminDashboardStatsHandler(ISubscriptionPlanRepo _subscriptionPlanRepo) : ResponseHandler, 
        IRequestHandler<GetAdminDashboardStatsQuery, Response<AdminDashboardStatsResponse>>
    {
        public async Task<Response<AdminDashboardStatsResponse>> Handle(GetAdminDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = await _subscriptionPlanRepo.GetTableNoTracking()
                .Include(x => x.Plan)
                .ToListAsync(cancellationToken);

            var response = new AdminDashboardStatsResponse
            {
                TotalRevenue = subscriptions.Where(x => x.Status == SubscriptionStatus.Active).Sum(x => x.Plan.Price),
                TotalSubscribers = subscriptions.Select(x => x.DoctorId).Distinct().Count(),
                ActiveSubscribers = subscriptions.Count(x => x.Status == SubscriptionStatus.Active),
                NewSubscribersToday = subscriptions.Count(x => x.StartDate >= DateTime.UtcNow.Date)
            };

            return Success(response);
        }
    }
}
