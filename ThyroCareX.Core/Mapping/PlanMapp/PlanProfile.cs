using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Mapping.PlanMapp
{
    public partial class PlanProfile:Profile
    {
        public PlanProfile() 
        {
            PlanQueryMapping();
            PlanCommandMapping();
        }

    }
}
