using MediatR;
using AutoMapper;
using ThyroCareX.Core.Bases;
using ThyroCareX.Service.Abstarct;
using ThyroCareX.Core.Feature.Community.Queries.Models;
using ThyroCareX.Core.Feature.Community.Queries.Result;

namespace ThyroCareX.Core.Feature.Community.Queries.Handler
{
    public class PostsQueryHandler : ResponseHandler, IRequestHandler<GetAllPostsQuery, Response<List<GetAllPostsResponse>>>
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsQueryHandler(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        public async Task<Response<List<GetAllPostsResponse>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postService.GetAllPostsAsync();
            var postsList = _mapper.Map<List<GetAllPostsResponse>>(posts);
            return Success(postsList);
        }
    }
}
