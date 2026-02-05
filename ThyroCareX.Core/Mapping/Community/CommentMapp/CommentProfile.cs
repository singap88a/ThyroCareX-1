using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Mapping.Community.CommentMapp
{
    public partial class CommentProfile:Profile
    {
        public CommentProfile() 
        {
            AddCommentCommandMapp();
            GetAllCommentsMapp();
        }
    }
}
