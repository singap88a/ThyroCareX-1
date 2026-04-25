using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Patients.Command.Model;
using ThyroCareX.Data.Models;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.Patients.Command.Handler
{
    public class PatientCommandHandler : ResponseHandler, IRequestHandler<AddPatientCommand, Response<string>>
                                                      , IRequestHandler<EditPatientCommand, Response<string>>
    {
        #region Fields
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IUserContextService _userContextService;

        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public PatientCommandHandler(IPatientService patientService, IUserContextService userContextService,IMapper mapper, IDoctorService doctorService)
        {

            _patientService = patientService;
            _doctorService = doctorService;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {
            var userIdString = _userContextService.UserId;

            if (string.IsNullOrEmpty(userIdString))
                return new Response<string>("Unauthorized");

            if (!int.TryParse(userIdString, out var userId))
                return new Response<string>("Invalid UserId");

            var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);
            var patient = _mapper.Map<Patient>(request);
            patient.DoctorID= doctor.DoctorID;


            var result = await _patientService.AddAsync(patient);
            return Success(result);






        }

        public async Task<Response<string>> Handle(EditPatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientService.GetPatientByIdAsync(request.PatientID);
            if (patient == null)
            {
                return NotFound<string>("Patient not found");
            }

            // Map the Requst to Patient entity
            var PatientMapper = _mapper.Map<Patient>(request);
            // Call the Service to Update Patient
            var result = await _patientService.EditAsync(PatientMapper);
            // Return Response
            return Success(result);
        }

        #endregion
    }
}
