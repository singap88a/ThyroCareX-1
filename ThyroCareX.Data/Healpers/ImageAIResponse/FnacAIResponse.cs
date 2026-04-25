using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Dto.FnacAIResponse
{
    public class FnacAIResponse
    {
        public string Status { get; set; }
        public FnacClassificationDto? Classification { get; set; }
        public string? Message { get; set; }
        
        [JsonPropertyName("session_id")]
        public string? SessionId { get; set; }

        [JsonPropertyName("medical_disclaimer")]
        public string? MedicalDisclaimer { get; set; }
    }

    public class FnacClassificationDto
    {
        public int Prediction { get; set; }

        [JsonPropertyName("bethesda_category")]
        public string BethesdaCategory { get; set; }

        [JsonPropertyName("bethesda_label")]
        public string BethesdaLabel { get; set; }

        [JsonPropertyName("confidence_pct")]
        public double Confidence { get; set; }

        [JsonPropertyName("malignancy_risk")]
        public string MalignancyRisk { get; set; }

        public string Recommendation { get; set; }

        [JsonPropertyName("needs_manual_review")]
        public bool NeedsManualReview { get; set; }
    }
}
