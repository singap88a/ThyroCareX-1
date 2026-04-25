using Microsoft.Extensions.Logging;
using ThyroCareX.Core.Dto.FnacAIResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Enums;
using ThyroCareX.Data.Healpers.ClinicalAI;
using ThyroCareX.Data.Models;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Service.Impelemanation
{
    public class TestProcessingJob : ITestProcessingJob
    {
        private readonly ITestService _testService;
        private readonly IAIService _ai;
        private readonly ILogger<TestProcessingJob> _logger;

        public TestProcessingJob(ITestService testService, IAIService ai, ILogger<TestProcessingJob> logger)
        {
            _testService = testService;
            _ai = ai;
            _logger = logger;
        }

        public async Task ProcessAsync(int testId)
        {
            Test? test = null;
            try
            {
                test = await _testService.GetTestByIdWithPatientAsync(testId);
                if (test == null)
                {
                    _logger.LogWarning("Test with ID {TestId} not found.", testId);
                    return;
                }

                test.Status = TestStatus.Processing;
                await _testService.UpdateTestAsync(test);

                // 🖼️ 1. Image AI
                var imageResult = await _ai.PredictImageAsync(test.ImagePath);

                // 🧠 2. Clinical AI
                int age = CalculateAge(test.Patient.DateOfBirth);

                var clinicalResult = await _ai.AssessClinicalAsync(new ClinicalRequest
                {
                    PatientId = test.PatientId,
                    Age = age,
                    OnThyroxine = test.OnThyroxine,
                    ThyroidSurgery = test.ThyroidSurgery,
                    QueryHyperthyroid = test.QueryHyperthyroid,
                    TSH = test.TSH,
                    T3 = test.T3,
                    TT4 = test.TT4,
                    FTI = test.FTI,
                    T4U = test.T4U,
                    NodulePresent = test.NodulePresent
                });

                // 🔬 3. FNAC AI (Optional)
                FnacAIResponse? fnacResult = null;
                if (!string.IsNullOrEmpty(test.FnacImagePath))
                {
                    fnacResult = await _ai.PredictFnacAsync(test.FnacImagePath);
                }

                // 🔗 4. Save Diagnosis
                var diagnosis = new DiagnosisResult
                {
                    TestId = test.Id,
                    ClassificationLabel = imageResult.Classification.Label,
                    Confidence = imageResult.Classification.Confidence,
                    TiradsStage = imageResult.Classification.Tirads_Stage,
                    OverlayImageUrl = imageResult.Images.Overlay_Url,
                    MaskImageUrl = imageResult.Images.Mask_Url,
                    RoiImageUrl = imageResult.Images.Roi_Url,

                    FunctionalStatus = clinicalResult.FunctionalStatus,
                    RiskLevel = clinicalResult.RiskLevel,
                    ClinicalRecommendation = clinicalResult.ClinicalRecommendation,
                    NextStep = clinicalResult.NextStep,

                    BethesdaCategory = fnacResult?.Classification?.BethesdaCategory,
                    BethesdaLabel = fnacResult?.Classification?.BethesdaLabel,
                    MalignancyRisk = fnacResult?.Classification?.MalignancyRisk,
                    FnacRecommendation = fnacResult?.Classification?.Recommendation
                };

                await _testService.SaveDiagnosisAsync(diagnosis);

                test.Status = TestStatus.Completed;
                await _testService.UpdateTestAsync(test);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing test {TestId}", testId);
                if (test != null)
                {
                    test.Status = TestStatus.Failed;
                    await _testService.UpdateTestAsync(test);
                }
            }
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
    

