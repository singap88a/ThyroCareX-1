using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Doctors.Queires.Result;
using ThyroCareX.Core.Feature.Plans.Queries.Models;
using ThyroCareX.Core.Feature.Plans.Queries.Result;
using ThyroCareX.Service.Abstarct;

using ThyroCareX.Data.Enums;
using Microsoft.EntityFrameworkCore;
using ThyroCareX.Infrastructure.Abstarct;

namespace ThyroCareX.Core.Feature.Plans.Queries.Handler
{
    public class PlanQueryHandler(IPlanService _palnService, IMapper mapper, ISubscriptionPlanRepo _subRepo) : ResponseHandler, IRequestHandler<PlanListQuery, Response<List<PlanListResponse>>>,
                                                                IRequestHandler<SinglePlanQuery, Response<SinglePlanResponse>>
    {

        public async Task<Response<List<PlanListResponse>>> Handle(PlanListQuery request, CancellationToken cancellationToken)
        {
            var plans = await _palnService.DisplayAllPlans();
            if (plans == null)
            {
                return NotFound<List<PlanListResponse>>("No Plans Found");
            }
            
            var plansMap = mapper.Map<List<PlanListResponse>>(plans);
            
            // Fetch all subscriptions to calculate stats
            var subscriptions = await _subRepo.GetTableNoTracking().ToListAsync(cancellationToken);
            
            foreach (var planDto in plansMap)
            {
                var planSubs = subscriptions.Where(s => s.PlanId == planDto.Id).ToList();
                
                // Real subscribers count (Active or Trialing)
                planDto.SubscribersCount = planSubs.Count(s => s.Status == SubscriptionStatus.Active || s.Status == SubscriptionStatus.Trialing);
                
                // Total revenue calculation based on paid subscriptions
                // Formula: count of 'Active' subscriptions * plan price (simplification for historical tracking)
                // In a production app, we would sum successful Payment transactions specifically for this plan.
                planDto.TotalRevenue = planSubs.Where(s => s.Status == SubscriptionStatus.Active).Sum(s => planDto.Price);
            }
            
            return Success(plansMap);
        }

        public async Task<Response<SinglePlanResponse>> Handle(SinglePlanQuery request, CancellationToken cancellationToken)
        {
            var Plan = await _palnService.GetPlanById(request.Id);
            if (Plan == null) return NotFound<SinglePlanResponse>("Plan Not Found");

            var PlanMapp = mapper.Map<SinglePlanResponse>(Plan);
            return Success(PlanMapp);
        }
    }
}
