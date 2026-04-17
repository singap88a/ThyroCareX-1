using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Enums;

namespace ThyroCareX.Core.Feature.Plans.Queries.Result
{
    public class PlanListResponse
    {
        public int Id { get; set; }
        public EnumPlan PlanType { get; set; }
        public string? Description { get; set; } 
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }

        public bool IsActive { get; set; }
    }
}
