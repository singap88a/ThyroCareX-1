using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Doctors.Queires.Result;
using ThyroCareX.Data.Models;
using ThyroCareX.Data.Models.Identity;

namespace ThyroCareX.Core.Mapping.DoctorMapp
{
    public partial class DoctorProfiles
    {
        public void GetDoctorListMapping()
        {
            CreateMap<Doctor,GetDoctorListResponse>()
                        .ForMember(dest => dest.SubscriptionPlanNames,
                           opt => opt.MapFrom(src => src.SubscriptionPlans.Select(sp => sp.Plan.Name).ToList()))
                        .ForMember(dest => dest.ImagePath,
                           opt => opt.MapFrom(src => src.ImagePath != null ? src.ImagePath : "default-doctor.png"))
                          .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization))
                         .ForMember(dest => dest.ProfileImage,
                           opt => opt.MapFrom(src => src.ProfileImage != null ? src.ProfileImage : "default-doctor.png"))
                         .ForMember(dest=>dest.MedicalLicenseNumber,opt=>opt.MapFrom(src=>src.MedicalLicenseNumber))
                         .ForMember(dest=>dest.ProfessionalBio,opt=>opt.MapFrom(src=>src.Bio));


            CreateMap<User, GetDoctorListResponse>()
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization));

        }
    }
   
}
