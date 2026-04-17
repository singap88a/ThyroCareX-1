using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThyroCareX.Data.Enums;

namespace ThyroCareX.Data.Models
{
    public class SubscriptionPlan
    {
        public SubscriptionPlan()
        {
         
        }
        [Key]
        public int SubscriptionPlanID { get; set; }
        public BillingPeriod BillingPeriod { get; set; }
        public SubscriptionStatus Status { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int PlanId { get; set; }
        [ForeignKey("PlanId")]
        public Plan Plan { get; set; } 

        public int? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; } = null!;
        public string? OrderId { get; set; }
        public string? TransactionId { get; set; }
    }
}

