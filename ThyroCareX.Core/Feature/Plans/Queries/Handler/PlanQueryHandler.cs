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

namespace ThyroCareX.Core.Feature.Plans.Queries.Handler
{
    public class PlanQueryHandler(IPlanService _palnService, IMapper mapper) : ResponseHandler, IRequestHandler<PlanListQuery, Response<List<PlanListResponse>>>,
                                                                IRequestHandler<SinglePlanQuery, Response<SinglePlanResponse>>
    {

        public async Task<Response<List<PlanListResponse>>> Handle(PlanListQuery request, CancellationToken cancellationToken)
        {
            var Plans = await _palnService.DisplayAllPlans();
            if (Plans == null)
            {
                return NotFound<List<PlanListResponse>>("No Plans Found");
            }
            var PlansMapp = mapper.Map<List<PlanListResponse>>(Plans);
            return Success(PlansMapp);

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
