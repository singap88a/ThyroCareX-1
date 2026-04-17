using MediatR;
using Stripe.V2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Payment.Commands.Models;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.Payment.Commands.Handler
{
    public class CreatePaymentCommandHandler : ResponseHandler, IRequestHandler<CreatePaymentCommandRequest, Response<string>>
    {
        private readonly IPaymentService _paymentService;
        private readonly IUserContextService _userContextService;
        private readonly IDoctorService _doctorService;

        public CreatePaymentCommandHandler(IPaymentService paymentService, IUserContextService userContextService, IDoctorService doctorService)
        {
            _paymentService = paymentService;
            _userContextService = userContextService;
            _doctorService = doctorService;
        }

        public async Task<Response<string>> Handle(CreatePaymentCommandRequest request, CancellationToken cancellationToken)
        {
            //var UserId = int.Parse( _userContextService.UserId);
            //var doctorId = await _doctorService.GetDoctorByUserIdAsync(UserId);
            var Payment= await _paymentService.CreatePayment(request.PlanId, request.DoctorId);
            return Success(Payment);

        }
    }
}