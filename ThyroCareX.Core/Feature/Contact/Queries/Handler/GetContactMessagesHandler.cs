using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Contact.Queries.Model;
using ThyroCareX.Core.Feature.Contact.Queries.Result;
using ThyroCareX.Infrastructure.Abstarct;

namespace ThyroCareX.Core.Feature.Contact.Queries.Handler
{
    public class GetContactMessagesHandler : ResponseHandler, 
        IRequestHandler<GetContactMessagesQuery, Response<List<ContactMessageResponse>>>
    {
        private readonly IContactRepo _contactRepo;

        public GetContactMessagesHandler(IContactRepo contactRepo)
        {
            _contactRepo = contactRepo;
        }

        public async Task<Response<List<ContactMessageResponse>>> Handle(GetContactMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _contactRepo.GetTableNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new ContactMessageResponse
                {
                    Id = x.Id,
                    Name = x.FullName,
                    Email = x.Email,
                    Subject = x.Subject,
                    Message = x.Message,
                    AttachmentUrl = x.AttachmentUrl,
                    IsReplied = x.IsReplied,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return Success(messages);
        }
    }
}
