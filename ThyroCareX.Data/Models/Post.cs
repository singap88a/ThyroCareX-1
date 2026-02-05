using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ThyroCareX.Data.Models
{
    public class Post
    {
        public Post()
        {
            PostLikes = new HashSet<PostLike>();
            Comments = new HashSet<Comment>();
        }
        [Key]
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string? ImagePost { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }=null!;

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }


    }
}
