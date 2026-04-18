using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Contact.Commands.Model;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Service.Abstarct;
using System.Threading;
using System.Threading.Tasks;

namespace ThyroCareX.Core.Feature.Contact.Commands.Handler
{
    public class SubmitContactMessageHandler : ResponseHandler, 
        IRequestHandler<SubmitContactMessageCommand, Response<string>>
    {
        private readonly IContactRepo _contactRepo;
        private readonly IImageService _imageService;

        public SubmitContactMessageHandler(IContactRepo contactRepo, IImageService imageService)
        {
            _contactRepo = contactRepo;
            _imageService = imageService;
        }

        public async Task<Response<string>> Handle(SubmitContactMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string? attachmentUrl = null;
                if (request.Attachment != null)
                {
                    using var stream = request.Attachment.OpenReadStream();
                    attachmentUrl = await _imageService.UploadImageAsync(stream, request.Attachment.FileName, "contacts");
                }

                var contactMessage = new ThyroCareX.Data.Models.Contact
                {
                    FullName = request.Name,
                    Email = request.Email,
                    Subject = request.Subject,
                    Message = request.Message,
                    AttachmentUrl = attachmentUrl,
                    CreatedAt = DateTime.UtcNow,
                    IsReplied = false
                };

                await _contactRepo.AddAsync(contactMessage);

                return Success<string>("Message sent successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }
        }
    }
}
