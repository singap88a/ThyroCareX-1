using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Dto
{
    public class PatientDto
    {
        public string FullName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Gender { get; private set; }

        public double Height { get; private set; }
        public double Weight { get; private set; }

        public string PhoneNumber { get; private set; }
        public string? Address { get; private set; }
    }
}
