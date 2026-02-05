using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Community.Command.Model
{
    public class AddCommentCommand:IRequest<Response<string>>
    {
        public int PostId { get; set; }
        public string Content { get; set; }
    }
}
