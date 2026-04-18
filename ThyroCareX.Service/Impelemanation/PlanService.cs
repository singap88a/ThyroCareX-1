using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Infrastructure.Repository;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Service.Impelemanation
{
    public class PlanService:IPlanService
    {
        private readonly IPlanRepo _planRepo;
        public PlanService(IPlanRepo planRepo) 
        {
            _planRepo = planRepo;
        }

        public async Task<string> AddPlan(Plan plan)
        {
            await _planRepo.AddAsync(plan);
            return "Plan added successfully";
        }

        public async Task<string> DeletePlanAsync(Plan plan)
        {
            var trans = _planRepo.BeginTransaction();
            try
            {
                await _planRepo.DeleteAsync(plan);
                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }

        public Task<List<Plan>> DisplayAllPlans()
        {
            var plans = _planRepo.GetAllPlanAsync();
           return plans;
        }

        public async Task<Plan> GetPlanById(int id)
        {
            return await _planRepo.GetByIdAsync(id);
        }

        public async Task<string> UpdatePlanAsync(Plan plan)
        {
            await _planRepo.UpdateAsync(plan);
            return "Success";
        }
    }

}
