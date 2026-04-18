using MediatR;
using System.Collections.Generic;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Contact.Queries.Result;

namespace ThyroCareX.Core.Feature.Contact.Queries.Model
{
    public class GetContactMessagesQuery : IRequest<Response<List<ContactMessageResponse>>>
    {
    }
}
