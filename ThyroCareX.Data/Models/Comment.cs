using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; } = null!;

        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }= null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
