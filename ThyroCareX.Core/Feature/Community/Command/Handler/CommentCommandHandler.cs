using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Community.Command.Model;
using ThyroCareX.Data.Models;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.Community.Command.Handler
{
    public class CommentCommandHandler: ResponseHandler, IRequestHandler<AddCommentCommand,Response<string>>
    {
        #region Fieldes
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IDoctorService _doctorService;
        private readonly IPostService _postService;

        #endregion
        #region Cons
        public CommentCommandHandler(ICommentService commentService, IMapper mapper
                                     , IUserContextService userContextService, IDoctorService doctorService
                                     , IPostService postService)
        {
            _commentService = commentService;
            _mapper = mapper;
            _userContextService = userContextService;
            _doctorService = doctorService;
            _postService = postService;
            
        }

        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var userIdString = _userContextService.UserId;

            if (string.IsNullOrEmpty(userIdString))
                return new Response<string>("Unauthorized");

            if (!int.TryParse(userIdString, out var userId))
                return new Response<string>("Invalid UserId");

            var post = await _postService.GetPostByIdAsync(request.PostId);
            if (post == null) return new Response<string>("Post not found for this user");

            var doctor= await _doctorService.GetDoctorByUserIdAsync(userId);
            if (doctor == null) return new Response<string>("Doctor not found for this user");


            var comment = _mapper.Map<Comment>(request);
            comment.DoctorId = doctor.DoctorID;
            comment.PostId = post.Id;

            var result=await _commentService.AddCommentAsync(comment);

            return Success("Comment Added Successfully");

        }

        #endregion

    }
}
