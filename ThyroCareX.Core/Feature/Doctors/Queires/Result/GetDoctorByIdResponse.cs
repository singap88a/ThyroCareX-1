using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Enums;

namespace ThyroCareX.Core.Feature.Doctors.Queires.Result
{
    public class GetDoctorByIdResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public Gender gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; }
        public string MedicaLicenseNumber { get; set; }
        public string Address { get; set; }
        public string ProfessionalBio { get; set; }
        public string Hospital { get; set; }
        public string IdentificationImage { get; set; }
        public string? ProfileImage { get; set; }
        public DateTime RegistrationAt { get; set; }
    }
}
