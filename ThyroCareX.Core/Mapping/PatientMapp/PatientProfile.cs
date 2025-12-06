using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Mapping.PatientMapp
{
    public partial class PatientProfile:Profile
    {
        public PatientProfile() 
        {
            GetPatientListMapping();
            GetPatientListByDoctorMapping();
            GetPatientByIdMapping();
            AddPatientCommandMapping();
            UpdatePatientCommandMapp();
        }
    }
}
