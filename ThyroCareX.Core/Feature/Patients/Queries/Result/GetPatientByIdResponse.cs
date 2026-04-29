using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Enums;

namespace ThyroCareX.Core.Feature.Patients.Queries.Result
{
    public class GetPatientByIdResponse
    {
        public int PatientID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public Gender gender { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public DateTime RegistrationAt { get; set; } = DateTime.UtcNow;
        public int? DoctorID { get; set; }

        // Admission / Patient details
        public DateTime DateOfBirth { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string MedicalHistory { get; set; } = string.Empty;
        public string CurrentMedications { get; set; } = string.Empty;
        public string KnownAllergies { get; set; } = string.Empty;
        public string? AttachmentPath { get; set; }
    }
}
