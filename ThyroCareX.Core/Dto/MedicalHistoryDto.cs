using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Dto
{
    public class MedicalHistoryDto
    {
        public string History { get; private set; } = string.Empty;
        public string Medications { get; private set; } = string.Empty;
        public string Allergies { get; private set; } = string.Empty;
        public string AttachmentPath { get; set; } = string.Empty;
    }
}
