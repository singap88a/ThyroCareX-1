using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Plans.Commands.Model;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.Plans.Commands.Handler
{
    public class PlanHandler(IMapper mapper, IPlanService _planService) : ResponseHandler, 
        IRequestHandler<AddPlanCommand, Response<string>>,
        IRequestHandler<UpdatePlanCommand, Response<string>>,
        IRequestHandler<DeletePlanCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(AddPlanCommand request, CancellationToken cancellationToken)
        {
            var plan = mapper.Map<Plan>(request);
            var result = await _planService.AddPlan(plan);
            return Success(result, "Plan added successfully");
        }

        public async Task<Response<string>> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planService.GetPlanById(request.Id);
            if (plan == null) return NotFound<string>("Plan not found");

            mapper.Map(request, plan);
            var result = await _planService.UpdatePlanAsync(plan);
            return Success(result, "Plan updated successfully");
        }

        public async Task<Response<string>> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planService.GetPlanById(request.Id);
            if (plan == null) return NotFound<string>("Plan not found");

            var result = await _planService.DeletePlanAsync(plan);
            return Success(result, "Plan deleted successfully");
        }
    }
}
