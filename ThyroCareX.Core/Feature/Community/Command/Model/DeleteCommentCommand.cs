
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Community.Command.Model
{
    public class DeleteCommentCommand:IRequest<Response<string>>
    {
        public int CommentId { get; set; }
        public DeleteCommentCommand(int commentId)
        {
            CommentId = commentId;
        }

    }
}
