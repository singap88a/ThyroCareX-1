using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Infrastructure.Context;
using ThyroCareX.Infrastructure.InfrastructureBases;

namespace ThyroCareX.Infrastructure.Repository
{
    public class PlanRepo: GenericRepositoryAsync<Plan>,IPlanRepo
    {
        private readonly DbSet<Plan> _plan;
        public PlanRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _plan = dbContext.Set<Plan>();

        }

        public Task<List<Plan>> GetAllPlanAsync()
        {
            return _plan.ToListAsync();
        }
    }
}
