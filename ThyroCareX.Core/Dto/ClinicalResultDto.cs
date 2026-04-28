using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Dto
{
    public class ClinicalResultDto
    {
        public string FunctionalStatus { get; set; }

        public string RiskLevel { get; set; }

        public string ClinicalRecommendation { get; set; }

        public string NextStep { get; set; }

        public Dictionary<string, double> Probabilities { get; set; }
    }
}
