using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Enums;

namespace ThyroCareX.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public PaymentStatus Status { get; set; }

        public int SubscriptionId { get; set; }
        [ForeignKey("SubscriptionId")]
        public SubscriptionPlan? subscriptionPlan { get; set; }
    }
}
