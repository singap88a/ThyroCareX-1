using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Data.Models
{
    public class Plan
    {
        public Plan()
        {
            
            Prices = new HashSet<PlanPrice>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsFree { get; set; }
        public bool IsActive { get; set; } 

        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }

        public ICollection<PlanPrice> Prices { get; set; } 
    }
}
