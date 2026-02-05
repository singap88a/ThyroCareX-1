using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.Community.Command.Model;
using ThyroCareX.Core.Feature.Community.Queries.Models;

namespace ThyroCareX.Controllers
{
    [Authorize(Roles = "Doctor")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : AppControllerBase
    {
       
        [HttpPost("Add-Post")]
        public async Task<IActionResult> AddPost([FromForm]AddPostCommand command)
        {
            var result=await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("Display-All-Posts")]
        public async Task<IActionResult> DisplayAllPosts()
        {
            var response = await Mediator.Send(new GetAllPostsQuery());
            return Ok(response);
        }


        [HttpPost("Add-Comment")]
        public async Task<IActionResult> AddComment([FromForm] AddCommentCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("PostId:{postId}/comments")]
        public async Task<IActionResult>DisplayComments(int postId)
        {
            var response = await Mediator.Send(new GetAllCommentsQuery(postId));
            return Ok(response);

        }

        [HttpPost("Add-Post-Like")]
        public async Task<IActionResult> AddPostLike([FromForm] AddPostLikeCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }


    }
}
