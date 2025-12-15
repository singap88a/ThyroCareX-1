using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Mapping.AuthenticationMapp
{
    public partial class AuthenticationProfile:Profile
    {
        public AuthenticationProfile()
        {
            RegisterDoctorCommandMapp();
        }
    }
}
