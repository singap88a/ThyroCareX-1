using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Data.Healpers.ClinicalAIResponse;

namespace ThyroCareX.Core.Feature.TestWithAI.Commands.Models
{
    public class AssessClinicalCommand : IRequest<Response<ClinicalAIResponse>>
    {
        public int TestId { get; set; }
    }
}
