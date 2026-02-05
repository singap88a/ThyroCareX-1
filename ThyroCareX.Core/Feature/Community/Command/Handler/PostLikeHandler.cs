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
using ThyroCareX.Service.Impelemanation;

namespace ThyroCareX.Core.Feature.Community.Command.Handler
{
    public class PostLikeHandler:ResponseHandler, IRequestHandler<AddPostLikeCommand,Response<string>>
    {
        #region Fields
        private readonly IPostLikeService _postLikeService;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IDoctorService _doctorService;
        private readonly IPostService _postService;

        #endregion
        #region Cons
        public PostLikeHandler(IPostLikeService postLikeService, IMapper mapper
                               , IUserContextService userContextService,IDoctorService doctorService
                                , IPostService postService)

        {
            _postLikeService = postLikeService;
            _mapper = mapper;
            _userContextService = userContextService;
            _doctorService = doctorService;
            _postService = postService;
        }

        #endregion
        #region Handle Function
        public async Task<Response<string>> Handle(AddPostLikeCommand request, CancellationToken cancellationToken)
        {
            var userIdString = _userContextService.UserId;

            if (string.IsNullOrEmpty(userIdString))
                return new Response<string>("Unauthorized");

            if (!int.TryParse(userIdString, out var userId))
                return new Response<string>("Invalid UserId");

            var post = await _postService.GetPostByIdAsync(request.PostId);
            if (post == null) return new Response<string>("Post not found for this user");

            var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);
            if (doctor == null) return new Response<string>("Doctor not found for this user");
           
            var LikeMapper=_mapper.Map<PostLike>(request);
            LikeMapper.DoctorId = doctor.DoctorID;
           


            var AddLike = await _postLikeService.AddLikeAsync(LikeMapper);

            return Success(
         AddLike
             ? "Post liked"
            : "Post unliked"
    );


        }

        #endregion
    }
}
