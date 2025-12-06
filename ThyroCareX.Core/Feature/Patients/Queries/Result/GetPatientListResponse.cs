using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Enums;

namespace ThyroCareX.Core.Feature.Patients.Queries.Result
{
    public class GetPatientListResponse
    {
        public int PatientID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Gender gender { get; set; }
        public int Age { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationAt { get; set; } = DateTime.UtcNow;
        public int? DoctorID { get; set; }
    }
}
