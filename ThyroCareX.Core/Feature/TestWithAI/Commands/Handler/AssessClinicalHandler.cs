using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Data.Healpers.ClinicalAI;
using ThyroCareX.Data.Healpers.ClinicalAIResponse;
using ThyroCareX.Core.Feature.TestWithAI.Commands.Models;
using ThyroCareX.Data.Models;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.TestWithAI.Commands.Handler
{
    public class AssessClinicalHandler : ResponseHandler, IRequestHandler<AssessClinicalCommand, Response<ClinicalAIResponse>>
    {
        private readonly ITestService _testService;
        private readonly IAIService _aiService;

        public AssessClinicalHandler(ITestService testService, IAIService aiService)
        {
            _testService = testService;
            _aiService = aiService;
        }

        public async Task<Response<ClinicalAIResponse>> Handle(AssessClinicalCommand request, CancellationToken cancellationToken)
        {
            var test = await _testService.GetTestByIdWithPatientAsync(request.TestId);
            if (test == null) return NotFound<ClinicalAIResponse>("Test not found");

            var age = CalculateAge(test.Patient.DateOfBirth);
            var clinicalRequest = new ClinicalRequest
            {
                PatientId = test.PatientId,
                Age = age,
                OnThyroxine = test.OnThyroxine > 0 ? 1 : 0,
                ThyroidSurgery = test.ThyroidSurgery > 0 ? 1 : 0,
                QueryHyperthyroid = test.QueryHyperthyroid > 0 ? 1 : 0,
                TSH = test.TSH ?? 0,
                T3 = test.T3 ?? 0,
                TT4 = test.TT4 ?? 0,
                FTI = test.FTI ?? 0,
                T4U = test.T4U ?? 0,
                NodulePresent = test.NodulePresent
            };

            var aiResponse = await _aiService.AssessClinicalAsync(clinicalRequest);
            if (aiResponse == null || aiResponse.Status != "success")
                return BadRequest<ClinicalAIResponse>("AI Service failed to process clinical data");

            var diagnosis = await _testService.GetDiagnosisByTestIdAsync(request.TestId);
            if (diagnosis == null)
            {
                diagnosis = new DiagnosisResult
                {
                    TestId = request.TestId,
                    FunctionalStatus = aiResponse.FunctionalStatus,
                    RiskLevel = aiResponse.RiskLevel,
                    ClinicalRecommendation = aiResponse.ClinicalRecommendation,
                    NextStep = aiResponse.NextStep
                };
                await _testService.SaveDiagnosisAsync(diagnosis);
            }
            else
            {
                diagnosis.FunctionalStatus = aiResponse.FunctionalStatus;
                diagnosis.RiskLevel = aiResponse.RiskLevel;
                diagnosis.ClinicalRecommendation = aiResponse.ClinicalRecommendation;
                diagnosis.NextStep = aiResponse.NextStep;
                await _testService.UpdateDiagnosisAsync(diagnosis);
            }

            return Success(aiResponse);
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
