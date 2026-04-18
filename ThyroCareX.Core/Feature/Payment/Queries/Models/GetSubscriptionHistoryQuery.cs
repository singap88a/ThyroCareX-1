using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Data.Models;
using System;
using System.Collections.Generic;

namespace ThyroCareX.Core.Feature.Payment.Queries.Models
{
    public class SubscriptionHistoryDto
    {
        public int Id { get; set; }
        public string DoctorName { get; set; } = "";
        public string DoctorEmail { get; set; } = "";
        public int PlanType { get; set; }
        public decimal PlanPrice { get; set; }
        public int Status { get; set; } // 0=Pending, 1=Active, 2=Failed
        public DateTime? StartDate { get; set; }
        public string? TransactionId { get; set; }
        public string? OrderId { get; set; }
        public List<string> Features { get; set; } = new();
        public int DurationInDays { get; set; }
    }

    public class GetSubscriptionHistoryQuery : IRequest<Response<List<SubscriptionHistoryDto>>>
    {
    }
}

