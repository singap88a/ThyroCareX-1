using Microsoft.EntityFrameworkCore;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Infrastructure.Context;
using ThyroCareX.Infrastructure.InfrastructureBases;

namespace ThyroCareX.Infrastructure.Repository
{
    public class SubscriptionPlanRepo : GenericRepositoryAsync<SubscriptionPlan>, ISubscriptionPlanRepo
    {
        private readonly DbSet<SubscriptionPlan> _subscriptionPlans;
        public SubscriptionPlanRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _subscriptionPlans = dbContext.Set<SubscriptionPlan>();
        }
    }
}
