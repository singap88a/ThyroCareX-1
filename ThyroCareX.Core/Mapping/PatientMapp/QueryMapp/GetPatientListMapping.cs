using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Patients.Queries.Result;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Core.Mapping.PatientMapp
{
    public partial class PatientProfile
    {
        public void GetPatientListMapping()
        {
            CreateMap<Patient, GetPatientListResponse>()
                .ForMember(dest => dest.PatientID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RegistrationAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                    src.DateOfBirth == default
                        ? 0
                        : (int)((DateTime.UtcNow - src.DateOfBirth).TotalDays / 365.25)));
        }
    }
}
