using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Patients.Command.Model;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Core.Mapping.PatientMapp
{
    public partial class PatientProfile
    {
        public void AddPatientCommandMapping()
        {
            // Mapping configuration for AddPatientCommand can be added here if needed in the future.
            CreateMap<AddPatientCommand, Patient>();
        }
    }
}
