using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Mapping.Community.PostLikeMapp
{
    public partial class PostLikeProfile :Profile
    {
       public PostLikeProfile() 
        {
            AddPostLikeCommandMappers();
        }

    }
}
