using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Dto.ImageAIResponse;
using ThyroCareX.Core.Feature.TestWithAI.Commands.Models;
using ThyroCareX.Data.Models;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.TestWithAI.Commands.Handler
{
    public class PredictImageHandler : ResponseHandler, IRequestHandler<PredictImageCommand, Response<ImageAIResponse>>
    {
        private readonly ITestService _testService;
        private readonly IAIService _aiService;

        public PredictImageHandler(ITestService testService, IAIService aiService)
        {
            _testService = testService;
            _aiService = aiService;
        }

        public async Task<Response<ImageAIResponse>> Handle(PredictImageCommand request, CancellationToken cancellationToken)
        {
            var test = await _testService.GetTestByIdAsync(request.TestId);
            if (test == null) return NotFound<ImageAIResponse>("Test not found");

            if (string.IsNullOrEmpty(test.ImagePath))
                return BadRequest<ImageAIResponse>("No ultrasound image found for this test");

            var aiResponse = await _aiService.PredictImageAsync(test.ImagePath);
            if (aiResponse == null || aiResponse.Status != "success")
                return BadRequest<ImageAIResponse>("AI Service failed to process the image");

            var diagnosis = await _testService.GetDiagnosisByTestIdAsync(request.TestId);
            if (diagnosis == null)
            {
                diagnosis = new DiagnosisResult
                {
                    TestId = request.TestId,
                    ClassificationLabel = aiResponse.Classification.Label,
                    Confidence = aiResponse.Classification.Confidence,
                    TiradsStage = aiResponse.Classification.Tirads_Stage,
                    OverlayImageUrl = aiResponse.Images.Overlay_Url,
                    MaskImageUrl = aiResponse.Images.Mask_Url,
                    RoiImageUrl = aiResponse.Images.Roi_Url
                };
                await _testService.SaveDiagnosisAsync(diagnosis);
            }
            else
            {
                diagnosis.ClassificationLabel = aiResponse.Classification.Label;
                diagnosis.Confidence = aiResponse.Classification.Confidence;
                diagnosis.TiradsStage = aiResponse.Classification.Tirads_Stage;
                diagnosis.OverlayImageUrl = aiResponse.Images.Overlay_Url;
                diagnosis.MaskImageUrl = aiResponse.Images.Mask_Url;
                diagnosis.RoiImageUrl = aiResponse.Images.Roi_Url;
                await _testService.UpdateDiagnosisAsync(diagnosis);
            }

            return Success(aiResponse);
        }
    }
}
