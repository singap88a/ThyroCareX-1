using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Plans.Queries.Result;

namespace ThyroCareX.Core.Feature.Plans.Queries.Models
{
    public class SinglePlanQuery:IRequest<Response<SinglePlanResponse>>
    {
        public int Id { get; set; }
        public SinglePlanQuery(int id)
        {
            
            Id = id;
        }
    }
}
