using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Contact.Commands.Model;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Service.Abstarct;
using System.Threading;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Feature.Contact.Commands.Handler
{
    public class DeleteContactHandler : ResponseHandler, 
        IRequestHandler<DeleteContactCommand, Response<string>>
    {
        private readonly IContactRepo _contactRepo;
        private readonly IImageService _imageService;

        public DeleteContactHandler(IContactRepo contactRepo, IImageService imageService)
        {
            _contactRepo = contactRepo;
            _imageService = imageService;
        }

        public async Task<Response<string>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var message = await _contactRepo.GetByIdAsync(request.Id);
            if (message == null) return NotFound<string>("Message not found");

            // Delete associated file if exists
            if (!string.IsNullOrEmpty(message.AttachmentUrl))
            {
                _imageService.DeleteImage(message.AttachmentUrl, "contacts");
            }

            await _contactRepo.DeleteAsync(message);

            return Success<string>("Message deleted successfully");
        }
    }
}
