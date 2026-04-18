using MediatR;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Contact.Commands.Model
{
    public class DeleteContactCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
