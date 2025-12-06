using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Patients.Queries.Result;

namespace ThyroCareX.Core.Feature.Patients.Queries.Models
{
    public class GetPatientListQuery:IRequest<Response<List<GetPatientListResponse>>>
    {
    }
}
