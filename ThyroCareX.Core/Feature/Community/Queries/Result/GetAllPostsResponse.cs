using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Feature.Community.Queries.Result
{
    public class GetAllPostsResponse
    {
    
        public string DoctorName { get; set; }
        public string DoctorImage { get; set; }
        public string Specialization { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public string? ImagePost { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public bool IsLiked { get; set; }

    }
}
