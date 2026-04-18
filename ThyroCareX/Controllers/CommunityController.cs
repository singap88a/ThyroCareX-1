using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThyroCareX.Bases;
using ThyroCareX.Core.Feature.Community.Command.Model;
using ThyroCareX.Core.Feature.Community.Queries.Models;
using ThyroCareX.Core.Feature.Doctors.Commands.Models;
using ThyroCareX.Data.Models;
using ThyroCareX.Service.Abstarct;
using ThyroCareX.Infrastructure.Abstarct;
using Microsoft.EntityFrameworkCore;

namespace ThyroCareX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : AppControllerBase
    {
        private readonly ISubscriptionPlanRepo _subscriptionPlanRepo;
        private readonly IDoctorRepository _doctorRepo;
        private readonly IUserContextService _userContextService;

        public CommunityController(ISubscriptionPlanRepo subscriptionPlanRepo, IDoctorRepository doctorRepo, IUserContextService userContextService)
        {
            _subscriptionPlanRepo = subscriptionPlanRepo;
            _doctorRepo = doctorRepo;
            _userContextService = userContextService;
        }

        private async Task<bool> IsPremiumDoctorOrAdmin()
        {
            if (User.IsInRole("Admin")) return true;
            
            var userIdString = _userContextService.UserId;
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
                return false;
                
            var doctor = await _doctorRepo.GetTableNoTracking().FirstOrDefaultAsync(d => d.UserId == userId);
            if (doctor == null) return false;
            
            var hasActiveSub = await _subscriptionPlanRepo.GetTableNoTracking()
                .AnyAsync(s => s.DoctorId == doctor.DoctorID && s.Status == Data.Enums.SubscriptionStatus.Active && s.EndDate > DateTime.UtcNow);
                
            return hasActiveSub;
        }

        [Authorize(Roles = "Doctor")]

        [HttpPost("Add-Post")]
        public async Task<IActionResult> AddPost([FromForm]AddPostCommand command)
        {
            if (!await IsPremiumDoctorOrAdmin()) return StatusCode(403, "Active subscription required.");
            var result=await Mediator.Send(command);
            return Ok(result);
        }
        [Authorize(Roles = "Doctor,Admin")]

        [HttpGet("Display-All-Posts")]
        public async Task<IActionResult> DisplayAllPosts()
        {
            if (!await IsPremiumDoctorOrAdmin()) return StatusCode(403, "Active subscription required.");
            var response = await Mediator.Send(new GetAllPostsQuery());
            return Ok(response);
        }

        [Authorize(Roles = "Doctor")]

        [HttpPost("Add-Comment")]
        public async Task<IActionResult> AddComment([FromForm] AddCommentCommand command)
        {
            if (!await IsPremiumDoctorOrAdmin()) return StatusCode(403, "Active subscription required.");
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "Doctor,Admin")]

        [HttpGet("PostId:{postId}/comments")]
        public async Task<IActionResult>DisplayComments(int postId)
        {
            if (!await IsPremiumDoctorOrAdmin()) return StatusCode(403, "Active subscription required.");
            var response = await Mediator.Send(new GetAllCommentsQuery(postId));
            return Ok(response);

        }
        [Authorize(Roles = "Doctor")]

        [HttpPost("Add-Post-Like")]
        public async Task<IActionResult> AddPostLike([FromForm] AddPostLikeCommand command)
        {
            if (!await IsPremiumDoctorOrAdmin()) return StatusCode(403, "Active subscription required.");
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [Authorize(Roles = "Doctor,Admin")]

        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int CommentId)
        {
            if (!await IsPremiumDoctorOrAdmin()) return StatusCode(403, "Active subscription required.");
            var command = await Mediator.Send(new DeleteCommentCommand(CommentId));
            return Ok(command);

        }

        [Authorize(Roles= "Doctor,Admin")]
        [HttpDelete("post/{id}")]
        public async Task<IActionResult>DeletePost([FromRoute]int id)
        {
            if (!await IsPremiumDoctorOrAdmin()) return StatusCode(403, "Active subscription required.");
            var Response = await Mediator.Send(new DeletePostCommand(id));
            return Ok(Response);
        }



    }
}
