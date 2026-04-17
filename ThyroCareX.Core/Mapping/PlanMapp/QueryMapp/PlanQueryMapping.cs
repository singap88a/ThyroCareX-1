using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Plans.Queries.Result;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Core.Mapping.PlanMapp
{
    public partial class PlanProfile
    {
        public void PlanQueryMapping()
        {
            // Mapping configurations for Plan queries can be added here in the future
            
            CreateMap<Plan, PlanListResponse>();
            CreateMap<Plan, SinglePlanResponse>();

        }
    }
}
