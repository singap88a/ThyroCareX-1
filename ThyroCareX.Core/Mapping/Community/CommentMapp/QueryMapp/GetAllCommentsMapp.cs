using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Community.Queries.Result;
using ThyroCareX.Data.Models;


namespace ThyroCareX.Core.Mapping.Community.CommentMapp
{
    public partial class CommentProfile
    {
        public void GetAllCommentsMapp()
        {
            CreateMap<Comment, GetAllCommentsRespone>()
                .ForMember(dest => dest.CommentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName))
                .ForMember(dest => dest.DoctorImage, opt => opt.MapFrom(src => src.Doctor.ProfileImage))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Doctor.Specialization));
               
        }
    }
}
