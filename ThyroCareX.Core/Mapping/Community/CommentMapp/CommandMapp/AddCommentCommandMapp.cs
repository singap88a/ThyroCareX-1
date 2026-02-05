using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Community.Command.Model;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Core.Mapping.Community.CommentMapp
{
    public partial class CommentProfile
    {
        public void AddCommentCommandMapp()
        {
            CreateMap<AddCommentCommand,Comment>()
                  .ForMember(dest => dest.PostId, opt => opt.Ignore())
                  .ForMember(dest => dest.DoctorId, opt => opt.Ignore()); 
        }
    }
}
