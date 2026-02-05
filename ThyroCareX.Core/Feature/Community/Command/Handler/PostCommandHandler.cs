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
using ThyroCareX.Data.Models.Identity;
using ThyroCareX.Service.Abstarct;
using ThyroCareX.Service.Impelemanation;

namespace ThyroCareX.Core.Feature.Community.Command.Handler
{
    public class PostCommandHandler:ResponseHandler, IRequestHandler<AddPostCommand,Response<string>>
    {
        #region Prop
        private readonly IPostService _postService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IDoctorService _doctorService;

        #endregion
        #region Constructor
        public PostCommandHandler(IPostService postService, IMapper mapper
            , IImageService imageService, IUserContextService userContextService, IDoctorService doctorService)
        {
            _postService = postService;
            _mapper = mapper;
            _imageService= imageService;
            _userContextService = userContextService;
            _doctorService = doctorService;
            
        }

        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            var userIdString = _userContextService.UserId;

            if (string.IsNullOrEmpty(userIdString))
                return new Response<string>("Unauthorized");

            if (!int.TryParse(userIdString, out var userId))
                return new Response<string>("Invalid UserId");

            string? imagePath = null;

            if (request.ImagePost != null)
            {
                imagePath = await _imageService.UploadImageAsync(
                    request.ImagePost.OpenReadStream(),
                    request.ImagePost.FileName
                );
            }

            var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);
            if (doctor == null) return new Response<string>("Doctor not found for this user");

            var post = _mapper.Map<Post>(request);
          
            post.ImagePost = imagePath;
            post.DoctorId = doctor.DoctorID;

            var result= await _postService.AddPostAsync(post);
            return Success("Post Added Successfully");
           
        }
        #endregion
    }
}
