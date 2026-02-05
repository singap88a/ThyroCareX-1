using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Mapping.Community.PostMapp
{
   public partial class PostProfile:Profile
    {
        public PostProfile() 
        {
            AddPostCommandMappers();
            GetAllPostsQueryMapp();
        }
    }
}
