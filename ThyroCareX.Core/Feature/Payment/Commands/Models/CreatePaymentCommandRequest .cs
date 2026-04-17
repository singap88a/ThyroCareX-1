using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Payment.Commands.Models
{
    public class CreatePaymentCommandRequest : IRequest<Response<string>>
    {
        public int PlanId { get; set; }
        public int DoctorId { get; set; }
    }
}
