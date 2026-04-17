using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Data.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FullName { get; set; }=string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Subject {  get; set; }= string.Empty;
        public string Message {  get; set; }= string.Empty;
        public string File {  get; set; }= string.Empty;

        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }




        
    }
}
