using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Payment.Commands.Models
{
    public class PaymobCallbackCommand:IRequest
    {
        public JsonElement Body { get; set; }
        public string? Hmac { get; set; }
    }
}
