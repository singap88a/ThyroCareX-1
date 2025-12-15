using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Data.Enums;

namespace ThyroCareX.Core.Feature.AplicationUser.Command.Models
{
    public class AddUserCommand:IRequest<Response<string>>
    {
        public string FullName {  get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }
       public string ConfirmPassword {  get; set; }
       public string PhoneNumber { get; set; }
       public Gender Gender { get; set; }
       public DateTime DateofBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }       
        public string ZipCode { get; set; }     

    }
}
