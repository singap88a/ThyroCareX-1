using MediatR;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Contact.Commands.Model
{
    public class ToggleRepliedStatusCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
