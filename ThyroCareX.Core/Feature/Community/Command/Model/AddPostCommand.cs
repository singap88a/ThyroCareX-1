using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Community.Command.Model
{
    public class AddPostCommand : IRequest<Response<string>>
    {
        public string? Content { get; set; } 
        public IFormFile? ImagePost { get; set; }
    }
}
