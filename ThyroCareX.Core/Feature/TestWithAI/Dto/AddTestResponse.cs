using ThyroCareX.Core.Dto.FnacAIResponse;
using ThyroCareX.Core.Dto.ImageAIResponse;
using ThyroCareX.Data.Healpers.ClinicalAIResponse;

namespace ThyroCareX.Core.Feature.TestWithAI.Dto
{
    public class AddTestResponse
    {
        public int TestId { get; set; }
        public string Message { get; set; }
        public ImageAIResponse ImageResult { get; set; }
        public ClinicalAIResponse ClinicalResult { get; set; }
        public FnacAIResponse? FnacResult { get; set; }
    }
}
