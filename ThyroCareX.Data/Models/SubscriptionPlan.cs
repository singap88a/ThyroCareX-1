using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThyroCareX.Data.Enums;

namespace ThyroCareX.Data.Models
{
    public class SubscriptionPlan
    {
        public SubscriptionPlan()
        {
            Payments = new HashSet<Payment>();
        }
        [Key]
        public int SubscriptionPlanID { get; set; }
        public BillingPeriod BillingPeriod { get; set; }
        public SubscriptionStatus Status { get; set; }

        public string? StripeCustomerId { get; set; }
        public string? StripeSubscriptionId { get; set; }

        public DateTime? CurrentPeriodEnd { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        

        public int? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; } = null!;
        public  int? PlanId { get; set; }
        [ForeignKey("PlanId")]
        public Plan Plan { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }

    }
}
