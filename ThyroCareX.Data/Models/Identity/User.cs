using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ThyroCareX.Data.Models.Identity
{
    public class User : IdentityUser<int>
    {
        public string Address { get; set; }
        public string City { get; set; }
    }
}
