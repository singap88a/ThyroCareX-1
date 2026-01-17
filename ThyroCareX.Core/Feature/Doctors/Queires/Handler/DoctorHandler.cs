using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Doctors.Queires.Models;
using ThyroCareX.Core.Feature.Doctors.Queires.Result;
using ThyroCareX.Data.Models.Identity;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.Doctors.Queires.Handler
{
    public class DoctorHandler :ResponseHandler, IRequestHandler<GetDoctorListQuery,Response< List<GetDoctorListResponse>>>,
                                                 IRequestHandler<GetDoctorByIdQuery,Response<GetSingleDoctorResponse>>,
                                                 IRequestHandler<GetDoctorProfileQuery,Response<GetDoctorByIdResponse>>
    {
        #region Fields
        private readonly IDoctorService _doctorService;
         private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        #endregion
        #region Constructor
        public DoctorHandler(IDoctorService doctorService, IMapper mapper, IUserContextService userContextService)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<List<GetDoctorListResponse>>> Handle(GetDoctorListQuery request, CancellationToken cancellationToken)
        {
            var doctorList = await _doctorService.GetAllDoctorsAsync();
            var mappedDoctorList = _mapper.Map<List<GetDoctorListResponse>>(doctorList);
            return Success(mappedDoctorList);
        }

        public async Task<Response<GetSingleDoctorResponse>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var Doctor = await _doctorService.GetDoctorByIdWithIncludeAsync(request.Id);
            if(Doctor == null)return NotFound<GetSingleDoctorResponse>("Docotr Not Found");
            var result=_mapper.Map<GetSingleDoctorResponse>(Doctor);
            return Success(result);
        }

        public async Task<Response<GetDoctorByIdResponse>> Handle(GetDoctorProfileQuery request, CancellationToken cancellationToken)
        {
            var userIdString = _userContextService.UserId;

            if (string.IsNullOrEmpty(userIdString))
                return NotFound<GetDoctorByIdResponse>("Unauthorized");

            // نحاول نعمل تحويل من string إلى int بأمان
            if (!int.TryParse(userIdString, out var userId))
                return BadRequest<GetDoctorByIdResponse>("Invalid UserId");

            var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);

            if (doctor == null)
                return NotFound<GetDoctorByIdResponse>("Doctor not found");

            var result = _mapper.Map<GetDoctorByIdResponse>(doctor);

            return Success(result);


        }

        #endregion

    }
}
