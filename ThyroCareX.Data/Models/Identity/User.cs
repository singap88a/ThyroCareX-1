using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Enums;


namespace ThyroCareX.Data.Models.Identity
{
    public class User : IdentityUser<int>
    {
      
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string ImagePath { get; set; }
        public string Specialization { get; set; }


    }
}
