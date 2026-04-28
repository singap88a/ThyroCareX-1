using AutoMapper;
using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Dto;
using ThyroCareX.Core.Feature.TestWithAI.Commands.Models;
using ThyroCareX.Core.Feature.TestWithAI.Dto;
using ThyroCareX.Data.Enums;
using ThyroCareX.Data.Healpers.ClinicalAI;
using ThyroCareX.Data.Healpers.ClinicalAIResponse;
using ThyroCareX.Data.Models;
using ThyroCareX.Service.Abstarct;
using ThyroCareX.Service.Impelemanation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ThyroCareX.Core.Feature.TestWithAI.Commands.Handler
{
    public class AssessClinicalHandler : ResponseHandler, IRequestHandler<AssessClinicalCommand, Response<AssessClinicalResponse>>
    {
        private readonly ITestService _testService;
        private readonly IAIService _aiService;
        private readonly IUserContextService _userContextService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _PatientService;
        private readonly IMapper _mapper;
        

        public AssessClinicalHandler(ITestService testService, IMapper mapper, IPatientService PatientService, IDoctorService doctorService, IAIService aiService,IUserContextService userContextService)
        {
            _testService = testService;
            _aiService = aiService;
            _userContextService = userContextService;
            _doctorService = doctorService;
            _PatientService = PatientService;
            _mapper = mapper;

        }

        public async Task<Response<AssessClinicalResponse>> Handle(AssessClinicalCommand request, CancellationToken cancellationToken)
        {
            var userIdString = _userContextService.UserId;
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
                return Unauthorized<AssessClinicalResponse>("Unauthorized");

            var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);



            var test = new Test
            {
                PatientId = request.ClinicalRequest.PatientId,
                OnThyroxine = request.ClinicalRequest.OnThyroxine,
                ThyroidSurgery = request.ClinicalRequest.ThyroidSurgery,
                QueryHyperthyroid = request.ClinicalRequest.QueryHyperthyroid,
                TSH = request.ClinicalRequest.TSH,
                T3 = request.ClinicalRequest.T3,
                TT4 = request.ClinicalRequest.TT4,
                FTI = request.ClinicalRequest.FTI,
                T4U = request.ClinicalRequest.T4U,
                NodulePresent = request.ClinicalRequest.NodulePresent,
                DoctorId = doctor.DoctorID
            };
            test.Status = TestStatus.Processing;

            await _testService.AddTestAsync(test);

            // 🧠 1. Call AI
            var aiResponse = await _aiService.AssessClinicalAsync(request.ClinicalRequest);

            // 💾 2. Save Diagnosis
            var diagnosis = new DiagnosisResult
            {
                TestId = test.Id,
                FunctionalStatus = aiResponse.FunctionalStatus,
                RiskLevel = aiResponse.RiskLevel,
                ClinicalRecommendation = aiResponse.ClinicalRecommendation,
                NextStep = aiResponse.NextStep
            };

            await _testService.SaveDiagnosisAsync(diagnosis);

            test.Status = TestStatus.Completed;
            await _testService.UpdateTestAsync(test);
            // 🎯 Mapping Response
            var response = new AssessClinicalResponse
            {
                TestId = test.Id,
                Status = aiResponse.Status,
                Clinical = new ClinicalResultDto
                {
                    FunctionalStatus = aiResponse.FunctionalStatus,
                    RiskLevel = aiResponse.RiskLevel,
                    ClinicalRecommendation = aiResponse.ClinicalRecommendation,
                    NextStep = aiResponse.NextStep,
                    Probabilities = aiResponse.Probabilities
                }
            };

            return Success(response);
        }
       

    }
}
