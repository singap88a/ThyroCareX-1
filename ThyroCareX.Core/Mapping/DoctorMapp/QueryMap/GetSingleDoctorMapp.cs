using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Doctors.Queires.Models;
using ThyroCareX.Core.Feature.Doctors.Queires.Result;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Core.Mapping.DoctorMapp
{
    public partial class DoctorProfiles
    {
        public void GetSinglrDoctorMapp()
        {
            CreateMap<Doctor,GetSingleDoctorResponse>()
                  .ForMember(dest => dest.SubscriptionPlanNames,
                    opt => opt.MapFrom(src => src.SubscriptionPlans.Select(sp => sp.Plan.Name).ToList())); 
        }

    }
}
