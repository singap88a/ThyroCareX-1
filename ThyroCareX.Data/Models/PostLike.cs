using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Data.Models
{
    public class PostLike
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; } = null!;

        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]

        public Doctor doctor { get; set; } = null!;

    }
}
