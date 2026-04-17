using ThyroCareX.Core.Feature.Community.Queries.Result;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Core.Mapping.Community.PostMapp
{
    public partial class PostProfile
    {
        public void GetAllPostsQueryMapp()
        {
            CreateMap<Post, GetAllPostsResponse>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName))
                .ForMember(dest => dest.DoctorImage, opt => opt.MapFrom(src => src.Doctor.ProfileImage))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Doctor.Specialization))
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.PostLikes.Count(x=>x.IsActive)))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count()))
               .ForMember(dest => dest.IsLiked, opt => opt.MapFrom((src, dest, destMember, context) =>
               {
                   if (context.TryGetItems(out var items) && items.TryGetValue("UserId", out var userIdObj))
                   {
                       var userId = (int)userIdObj;
                       return src.PostLikes.Any(pl => pl.DoctorId == userId && pl.IsActive);
                   }

                   return false;

                
               }));
        }
    }
}
