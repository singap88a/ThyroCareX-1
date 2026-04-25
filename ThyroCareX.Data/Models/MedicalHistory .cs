using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThyroCareX.Data.Models
{
    public class MedicalHistory
    {
        [Key]
        public int MedicalRecordID { get; set; }
        public string History { get; private set; }= string.Empty;
        public string Medications { get; private set; }=string.Empty;
        public string Allergies { get; private set; } = string.Empty;
        public string AttachmentPath { get; set; }= string.Empty;
        public DateTime RecordDate { get; set; } = DateTime.UtcNow;
        public int? PatientID { get; set; }
        [ForeignKey("PatientID")]
        public Patient? Patient { get; set; }
    
    }
}
