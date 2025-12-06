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
    public class PatientCommandHandler:ResponseHandler,IRequestHandler<AddPatientCommand,Response<string>>
                                                      ,IRequestHandler<EditPatientCommand,Response<string>>
    {
        #region Fields
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public PatientCommandHandler(IPatientService patientService, IMapper mapper)
        {
            
            _patientService = patientService;
            _mapper = mapper;
        }

        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {
            // Map the Requst to Patient entity
            var PatientMapper = _mapper.Map<Patient>(request);
            // Call the Service to Add Patient
            var result = await _patientService.AddAsync(PatientMapper);
            // Return Response
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
            var result =await _patientService.EditAsync(PatientMapper);
            // Return Response
            return Success(result);
        }

        #endregion
    }
}
