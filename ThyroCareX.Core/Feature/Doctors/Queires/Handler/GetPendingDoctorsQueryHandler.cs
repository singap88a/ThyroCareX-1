using AutoMapper;
using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Doctors.Queires.Models;
using ThyroCareX.Core.Feature.Doctors.Queires.Result;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.Doctors.Queires.Handler
{
    public class GetPendingDoctorsQueryHandler : ResponseHandler, IRequestHandler<GetPendingDoctorsQuery, Response<List<GetDoctorListResponse>>>
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public GetPendingDoctorsQueryHandler(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        public async Task<Response<List<GetDoctorListResponse>>> Handle(GetPendingDoctorsQuery request, CancellationToken cancellationToken)
        {
            var doctors = await _doctorService.GetPendingDoctorAsync();
            var mappedDoctors = _mapper.Map<List<GetDoctorListResponse>>(doctors);
            return Success(mappedDoctors);
        }
    }
}
