using System;
using System.ComponentModel.DataAnnotations;

namespace ThyroCareX.Data.Models
{
    public class WebhookLog
    {
        [Key]
        public int Id { get; set; }
        public string EventType { get; set; } = string.Empty;
        [MaxLength(255)]
        public string StripeEventId { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }
    }
}
