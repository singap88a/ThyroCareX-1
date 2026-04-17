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
    public class PlanHandler(IMapper mapper,IPlanService _planService) : ResponseHandler, IRequestHandler<AddPlanCommand, Response<string>>

    {
        public async Task<Response<string>> Handle(AddPlanCommand request, CancellationToken cancellationToken)
        {
           var plan = mapper.Map<Plan>(request);
            var result =await _planService.AddPlan(plan);
            return Success(result, "Plan added successfully");
          
        }
    }
}
