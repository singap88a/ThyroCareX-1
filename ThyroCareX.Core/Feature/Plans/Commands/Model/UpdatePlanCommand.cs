using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Data.Enums;

namespace ThyroCareX.Core.Feature.Plans.Commands.Model
{
    public class UpdatePlanCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public EnumPlan PlanType { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new List<string>();
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
    }
}
