using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Enums;

namespace ThyroCareX.Data.Models
{
    public class Test
    {
        public int Id { get; set; }

     

        // 🧪 Lab Data
        public double? TSH { get; set; }
        public double? T3 { get; set; }
        public double? TT4 { get; set; }
        public double? FTI { get; set; }
        public double? T4U { get; set; }

        public bool NodulePresent { get; set; }
        public int OnThyroxine { get; set; }
        public int ThyroidSurgery { get; set; }
        public int QueryHyperthyroid { get; set; }

        // 🖼️ Image
        public string? ImagePath { get; set; }
        public string? FnacImagePath { get; set; }

        // 📊 Status
        public TestStatus Status { get; set; } = TestStatus.Queued;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🔗 Navigation
        public DiagnosisResult DiagnosisResult { get; set; }


        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        public int PatientId { get;  set; }
        [ForeignKey("PatientId")]
        public Patient? Patient { get;  set; }
    }
}
