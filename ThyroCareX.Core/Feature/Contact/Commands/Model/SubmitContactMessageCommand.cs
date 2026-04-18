using MediatR;
using Microsoft.AspNetCore.Http;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Contact.Commands.Model
{
    public class SubmitContactMessageCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public IFormFile? Attachment { get; set; }
    }
}
