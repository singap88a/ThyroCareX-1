using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;

using ThyroCareX.Core.Feature.TestWithAI.Dto;

namespace ThyroCareX.Core.Feature.TestWithAI.Commands.Models
{
    public class AddTestCommand:IRequest<Response<AddTestResponse>>
    {
        public int PatientId { get; set; }

        public IFormFile Image { get; set; }
        public IFormFile? FnacImage { get; set; }

        public double? TSH { get; set; }
        public double? T3 { get; set; }
        public double? TT4 { get; set; }
        public double? FTI { get; set; }
        public double? T4U { get; set; }

        public bool NodulePresent { get; set; }
        public int OnThyroxine { get; set; }
        public int ThyroidSurgery { get; set; }
        public int QueryHyperthyroid { get; set; }
    }
}
