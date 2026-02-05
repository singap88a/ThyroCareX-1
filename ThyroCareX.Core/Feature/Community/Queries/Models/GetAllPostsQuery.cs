using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Community.Queries.Result;

namespace ThyroCareX.Core.Feature.Community.Queries.Models
{
    public class GetAllPostsQuery:IRequest<Response<List<GetAllPostsResponse>>>
    {
    }
}
