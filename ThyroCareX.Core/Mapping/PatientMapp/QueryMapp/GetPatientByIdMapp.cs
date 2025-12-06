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
        private void GetPatientByIdMapping()
        {
            CreateMap<Patient, GetPatientByIdResponse>();

               
        }
    }
}
