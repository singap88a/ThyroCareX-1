using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Authentication.Command.Models
{
    public class SignInCommand:IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
