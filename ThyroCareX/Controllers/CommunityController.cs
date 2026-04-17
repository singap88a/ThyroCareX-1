using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.Community.Command.Model;
using ThyroCareX.Core.Feature.Community.Queries.Models;
using ThyroCareX.Core.Feature.Doctors.Commands.Models;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : AppControllerBase
    {
        [Authorize(Roles = "Doctor")]

        [HttpPost("Add-Post")]
        public async Task<IActionResult> AddPost([FromForm]AddPostCommand command)
        {
            var result=await Mediator.Send(command);
            return Ok(result);
        }
        [Authorize(Roles = "Doctor,Admin")]

        [HttpGet("Display-All-Posts")]
        public async Task<IActionResult> DisplayAllPosts()
        {
            var response = await Mediator.Send(new GetAllPostsQuery());
            return Ok(response);
        }

        [Authorize(Roles = "Doctor")]

        [HttpPost("Add-Comment")]
        public async Task<IActionResult> AddComment([FromForm] AddCommentCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "Doctor,Admin")]

        [HttpGet("PostId:{postId}/comments")]
        public async Task<IActionResult>DisplayComments(int postId)
        {
            var response = await Mediator.Send(new GetAllCommentsQuery(postId));
            return Ok(response);

        }
        [Authorize(Roles = "Doctor")]

        [HttpPost("Add-Post-Like")]
        public async Task<IActionResult> AddPostLike([FromForm] AddPostLikeCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [Authorize(Roles = "Doctor,Admin")]

        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int CommentId)
        {
            var command = await Mediator.Send(new DeleteCommentCommand(CommentId));
            return Ok(command);

        }

        [Authorize(Roles= "Doctor,Admin")]
        [HttpDelete("post/{id}")]
        public async Task<IActionResult>DeletePost([FromRoute]int id)
        {
            var Response = await Mediator.Send(new DeletePostCommand(id));
            return Ok(Response);
        }



    }
}
