using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Plans.Commands.Model;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Core.Mapping.PlanMapp
{
    public partial class PlanProfile
    {
            public void PlanCommandMapping()
            {
            // Mapping configurations for Plan commands can be added here in the future
            CreateMap<AddPlanCommand,Plan>();
        }
    }
}
