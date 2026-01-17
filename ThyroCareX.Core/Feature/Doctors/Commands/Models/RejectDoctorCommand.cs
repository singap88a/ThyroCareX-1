using MediatR;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Doctors.Commands.Models
{
    public class RejectDoctorCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
