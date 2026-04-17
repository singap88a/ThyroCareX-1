using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Feature.Community.Queries.Result
{
    public class GetAllCommentsRespone
    {
        public int CommentId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorImage { get; set; }
        public string Specialization { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
    }
}
