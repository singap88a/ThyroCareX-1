using MediatR;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Plans.Commands.Model
{
    public class DeletePlanCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeletePlanCommand(int id) => Id = id;
    }
}
