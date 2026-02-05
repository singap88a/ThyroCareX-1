using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Community.Command.Model;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Core.Mapping.Community.PostLikeMapp
{
    public partial class PostLikeProfile
    {
        public void AddPostLikeCommandMappers()
        {
            CreateMap<AddPostLikeCommand, PostLike>()
                .ForMember(dest => dest.DoctorId, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());
        }
    }
}
