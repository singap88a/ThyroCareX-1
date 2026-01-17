using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Doctors.Queires.Result;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Core.Mapping.DoctorMapp
{
    public partial class DoctorProfiles
    {
        public async Task GetDoctorByIdMapp()
        {
            // Mapping configurations for GetDoctorById can be added here in the future
            CreateMap<Doctor, GetDoctorByIdResponse>()
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization))
                .ForMember(dest => dest.MedicaLicenseNumber, opt => opt.MapFrom(src => src.MedicalLicenseNumber))
                .ForMember(dest => dest.ProfessionalBio, opt => opt.MapFrom(src => src.Bio))
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber ?? string.Empty))
                 .ForMember(dest => dest.IdentificationImage, opt => opt.MapFrom(src => src.ImagePath))
                 .ForMember(dest => dest.ProfileImage,
                     opt => opt.MapFrom(src => src.ProfileImage != null ? src.ProfileImage : "default-doctor.png"));
        }

    }
}
