using MediatR;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Payment.Queries.Models
{
    public class AdminDashboardStatsResponse
    {
        public decimal TotalRevenue { get; set; }
        public int TotalSubscribers { get; set; }
        public int ActiveSubscribers { get; set; }
        public int NewSubscribersToday { get; set; }
    }

    public class GetAdminDashboardStatsQuery : IRequest<Response<AdminDashboardStatsResponse>>
    {
    }
}
