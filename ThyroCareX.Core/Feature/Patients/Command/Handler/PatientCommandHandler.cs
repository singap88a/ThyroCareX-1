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
    public class PatientCommandHandler : ResponseHandler,
     IRequestHandler<AddPatientCommand, Response<string>>,
     IRequestHandler<EditPatientCommand, Response<string>>,
     IRequestHandler<DeletePatientCommand, Response<string>>
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public PatientCommandHandler(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        // Add
        public async Task<Response<string>> Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {
            var patient = _mapper.Map<Patient>(request);
            var result = await _patientService.AddAsync(patient);
            return Success(result);
        }

        // Edit
        public async Task<Response<string>> Handle(EditPatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientService.GetPatientByIdAsync(request.PatientID);
            if (patient == null) return NotFound<string>("Patient not found");

            var mapped = _mapper.Map(request, patient);
            var result = await _patientService.EditAsync(mapped);
            return Success(result);
        }

        // Delete
        public async Task<Response<string>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientService.GetPatientByIdAsync(request.PatientID);
            if (patient == null) return NotFound<string>("Patient not found");

            var result = await _patientService.DeleteAsync(request.PatientID);
            return Success(result);
        }
    }

}
