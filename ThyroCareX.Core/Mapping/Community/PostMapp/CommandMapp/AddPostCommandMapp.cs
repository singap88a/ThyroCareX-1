using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Community.Command.Model;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Core.Mapping.Community.PostMapp
{
    public partial class PostProfile
    {
        public void AddPostCommandMappers() 
        {
            CreateMap<AddPostCommand, Post>()
                .ForMember(dest => dest.ImagePost, opt => opt.Ignore());

        }
    }
}
