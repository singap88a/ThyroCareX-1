using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Doctors.Commands.Models;
using ThyroCareX.Core.Feature.Doctors.Queires.Result;
using ThyroCareX.Data.Enums;
using ThyroCareX.Data.Models;
using ThyroCareX.Service.Abstarct;
using ThyroCareX.Service.Impelemanation;

namespace ThyroCareX.Core.Feature.Doctors.Commands.Handler
{
    public class DoctorCommandHandler:ResponseHandler,
                                                 IRequestHandler<EditDoctorCommand,Response<string>>,
                                                 IRequestHandler<DeleteDoctorCommand,Response<string>>,
                                                 IRequestHandler<AddDoctorCommand,Response<string>>,
                                                 IRequestHandler<ApproveDoctorCommand,Response<string>>,
                                                 IRequestHandler<RejectDoctorCommand,Response<string>>
    {
        #region prop
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IUserContextService _userContextService;

        #endregion
        #region Constructor
        public DoctorCommandHandler(IDoctorService doctorService, IMapper mapper,
            IImageService imageService, IUserContextService userContextService)
        {
            _doctorService = doctorService;
            _userContextService = userContextService;
            _imageService = imageService;
                        _mapper = mapper;

        }

        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(EditDoctorCommand request, CancellationToken cancellationToken)
        {
            var userIdString = _userContextService.UserId;

            if (string.IsNullOrEmpty(userIdString))
                return new Response<string>("Unauthorized");

            if (!int.TryParse(userIdString, out var userId))
                return new Response<string>("Invalid UserId");

            var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);

            if (doctor == null)
                return new Response<string>("Doctor not found");
            if (request.ProfileImage != null)
            {
                if (!string.IsNullOrEmpty(doctor.ProfileImage))
                {
                    _imageService.DeleteImage(doctor.ProfileImage);
                }
                var imagePath = await _imageService.UploadImageAsync(
                    request.ProfileImage.OpenReadStream(),
                    request.ProfileImage.FileName);

                doctor.ProfileImage = imagePath;
            }

            //map the request to doctor entity
            var result = _mapper.Map(request, doctor);
            await _doctorService.EditAsync(result);
            return Success("Doctor Updated Successfully");

           
        }

        public async Task<Response<string>> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            //check if the Id is Exist or Not
            var doctor= await _doctorService.GetDoctorByIdAsync(request.Id);
            //return NotFound
            if(doctor == null)
            {
                return NotFound<string>("Doctor Is Not Found");
            }
            //call service that make delete
            var result = await _doctorService.DeleteAsync(doctor);
            //return response
            return Success(result);

        }

        public async Task<Response<string>> Handle(AddDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctorMapped = _mapper.Map<Doctor>(request);
            var result = await _doctorService.AddAsync(doctorMapped);
            // return response
            return Success(result);
        }

        public async Task<Response<string>> Handle(ApproveDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(request.Id);

            if (doctor == null)
            {
                return NotFound<string>("Doctor Is Not Found");
            }

            doctor.Status=DoctorStatus.Approved;
            await _doctorService.EditAsync(doctor);
            return Success("Doctor Approved Successfully");


        }

        public async Task<Response<string>> Handle(RejectDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(request.Id);

            if (doctor == null)
            {
                return NotFound<string>("Doctor Is Not Found");
            }

            doctor.Status = DoctorStatus.Rejected;
            await _doctorService.EditAsync(doctor);
            return Success("Doctor Rejected Successfully");
        }


        #endregion
    }
}

