using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Enums;

namespace ThyroCareX.Data.Models
{
    public class PlanPrice
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public BillingPeriod BillingPeriod { get; set; }
        public string StripePriceId { get; set; } = null!;
        public int PlanId { get; set; }
        [ForeignKey("PlanId")]
        public Plan Plan { get; set; }
    }
}
