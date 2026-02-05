using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Community.Queries.Models;
using ThyroCareX.Core.Feature.Community.Queries.Result;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.Community.Queries.Handler
{
    public class CommentsQueryHandler:ResponseHandler, IRequestHandler<GetAllCommentsQuery, Response<List<GetAllCommentsRespone>>>
    {
        #region Feildes
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public CommentsQueryHandler(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<List<GetAllCommentsRespone>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(request.PostId);
            var mappedComments=_mapper.Map<List<GetAllCommentsRespone>>(comments);
            return Success(mappedComments);
        }
        #endregion
    }
}
