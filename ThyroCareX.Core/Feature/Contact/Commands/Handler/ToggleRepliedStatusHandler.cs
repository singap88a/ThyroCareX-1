using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Contact.Commands.Model;
using ThyroCareX.Infrastructure.Abstarct;
using System.Threading;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Feature.Contact.Commands.Handler
{
    public class ToggleRepliedStatusHandler : ResponseHandler, 
        IRequestHandler<ToggleRepliedStatusCommand, Response<string>>
    {
        private readonly IContactRepo _contactRepo;

        public ToggleRepliedStatusHandler(IContactRepo contactRepo)
        {
            _contactRepo = contactRepo;
        }

        public async Task<Response<string>> Handle(ToggleRepliedStatusCommand request, CancellationToken cancellationToken)
        {
            var message = await _contactRepo.GetByIdAsync(request.Id);
            if (message == null) return NotFound<string>("Message not found");

            message.IsReplied = !message.IsReplied;
            await _contactRepo.UpdateAsync(message);

            return Success<string>($"Status updated to {(message.IsReplied ? "Replied" : "Pending")}");
        }
    }
}
