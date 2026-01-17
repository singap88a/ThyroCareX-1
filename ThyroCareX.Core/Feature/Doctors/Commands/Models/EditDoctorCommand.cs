using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Data.Enums;

namespace ThyroCareX.Core.Feature.Doctors.Commands.Models
{
    public class EditDoctorCommand:IRequest<Response<string>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Gender gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; }
        public string MedicaLicenseNumber { get; set; }
        public string Address { get; set; }
        public string? ProfessionalBio { get; set; }
        public string? Hospital { get; set; }
        public IFormFile? ProfileImage { get; set; }


    }
}
