using AutoMapper;
using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Community.Queries.Models;
using ThyroCareX.Core.Feature.Community.Queries.Result;
using ThyroCareX.Service.Abstarct;
using ThyroCareX.Service.Impelemanation;

namespace ThyroCareX.Core.Feature.Community.Queries.Handler
{
    public class PostsQueryHandler : ResponseHandler, IRequestHandler<GetAllPostsQuery, Response<List<GetAllPostsResponse>>>
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IDoctorService _doctorService;

        public PostsQueryHandler(IPostService postService, IMapper mapper
             , IUserContextService userContextService, IDoctorService doctorService)
        {
            _postService = postService;
            _mapper = mapper;
            _userContextService = userContextService;
            _doctorService = doctorService;
        }

        public async Task<Response<List<GetAllPostsResponse>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {


            var userIdString = _userContextService.UserId;

            if (string.IsNullOrEmpty(userIdString))
                return new Response<List<GetAllPostsResponse>>("Unauthorized");

            if (!int.TryParse(userIdString, out var userId))
                return new Response<List<GetAllPostsResponse>>("Invalid UserId");

            var posts = await _postService.GetAllPostsAsync();

            if (_userContextService.Role == "Admin")
            {
                var postList = _mapper.Map<List<GetAllPostsResponse>>(posts);
                return Success(postList);
            }

            var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);

            if (doctor == null)
                return new Response<List<GetAllPostsResponse>>("Doctor not found");

            var postsList = _mapper.Map<List<GetAllPostsResponse>>(
                posts,
                opt => opt.Items["UserId"] = doctor.DoctorID
            );

            return Success(postsList);


        }

      
    }
}
