using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Dto
{
    public class AssessClinicalResponse
    {
        public int TestId { get; set; }

        public string Status { get; set; }

        public ClinicalResultDto Clinical { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
