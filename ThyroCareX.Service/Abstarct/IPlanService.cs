using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Service.Abstarct
{
    public interface IPlanService
    {
        Task<List<Plan>> DisplayAllPlans();
        Task<string>AddPlan(Plan plan);
        Task<Plan> GetPlanById(int id);
        Task<string> DeletePlanAsync(Plan plan);
    }
}
