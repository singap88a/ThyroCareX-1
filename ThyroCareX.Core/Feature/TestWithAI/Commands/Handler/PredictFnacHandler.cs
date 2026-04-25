using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Dto.FnacAIResponse;
using ThyroCareX.Core.Feature.TestWithAI.Commands.Models;
using ThyroCareX.Data.Models;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.TestWithAI.Commands.Handler
{
    public class PredictFnacHandler : ResponseHandler, IRequestHandler<PredictFnacCommand, Response<FnacAIResponse>>
    {
        private readonly ITestService _testService;
        private readonly IAIService _aiService;

        public PredictFnacHandler(ITestService testService, IAIService aiService)
        {
            _testService = testService;
            _aiService = aiService;
        }

        public async Task<Response<FnacAIResponse>> Handle(PredictFnacCommand request, CancellationToken cancellationToken)
        {
            var test = await _testService.GetTestByIdAsync(request.TestId);
            if (test == null) return NotFound<FnacAIResponse>("Test not found");

            if (string.IsNullOrEmpty(test.FnacImagePath))
                return BadRequest<FnacAIResponse>("No FNAC image found for this test");

            var aiResponse = await _aiService.PredictFnacAsync(test.FnacImagePath);
            if (aiResponse == null || aiResponse.Status != "success")
                return BadRequest<FnacAIResponse>("AI Service failed to process FNAC image");

            var diagnosis = await _testService.GetDiagnosisByTestIdAsync(request.TestId);
            if (diagnosis == null)
            {
                diagnosis = new DiagnosisResult
                {
                    TestId = request.TestId,
                    BethesdaCategory = aiResponse.Classification.BethesdaCategory,
                    BethesdaLabel = aiResponse.Classification.BethesdaLabel,
                    MalignancyRisk = aiResponse.Classification.MalignancyRisk,
                    FnacRecommendation = aiResponse.Classification.Recommendation
                };
                await _testService.SaveDiagnosisAsync(diagnosis);
            }
            else
            {
                diagnosis.BethesdaCategory = aiResponse.Classification.BethesdaCategory;
                diagnosis.BethesdaLabel = aiResponse.Classification.BethesdaLabel;
                diagnosis.MalignancyRisk = aiResponse.Classification.MalignancyRisk;
                diagnosis.FnacRecommendation = aiResponse.Classification.Recommendation;
                await _testService.UpdateDiagnosisAsync(diagnosis);
            }

            return Success(aiResponse);
        }
    }
}
