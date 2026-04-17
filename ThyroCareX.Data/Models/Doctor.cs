using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThyroCareX.Data.Enums;
using ThyroCareX.Data.Models.Identity;

namespace ThyroCareX.Data.Models
{
    public class Doctor
    {
        public Doctor()
        {
            Patients = new HashSet<Patient>();
            MedicalRecords = new HashSet<MedicalRecord>();
            SubscriptionPlans = new HashSet<SubscriptionPlan>();
            Posts= new HashSet<Post>();
            Comments= new HashSet<Comment>();
            PostLikes= new HashSet<PostLike>();


        }
        [Key]
        public int DoctorID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Specialization { get; set; }
        public Gender gender { get; set; }
        public int? NationalID { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateOnly DateofBirth { get; set; }
        public string? Hospital { get; set; }
        public string? MedicalLicenseNumber { get; set; }
        public string? Bio { get; set; }
        public string ImagePath { get; set; }
        public string? ProfileImage { get; set; }
        public DoctorStatus Status { get; set; }
        public DateTime RegistrationAt { get; set; }
        public int UserId { get; set; }

        // علاقة الـ Navigation property
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [InverseProperty("Doctor")]
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
        public virtual ICollection<SubscriptionPlan> SubscriptionPlans { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
         






    }
}
