using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Authentication.Command.Models;
using ThyroCareX.Data.Models;
using ThyroCareX.Data.Models.Identity;

namespace ThyroCareX.Core.Mapping.AuthenticationMapp
{
    public partial class AuthenticationProfile
    {
        public void RegisterDoctorCommandMapp()
        {
            // Add your mapping configurations here in the future
            CreateMap<RegisterDoctorCommand, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
                .ForMember(dest=>dest.Specialization,opt=>opt.MapFrom(src=>src.Specialization));

            CreateMap<RegisterDoctorCommand, Doctor>();


        }
    }
}
