using MediatR;
using ThyroCareX.Core.Bases;

namespace ThyroCareX.Core.Feature.Doctors.Commands.Models
{
    public class ApproveDoctorCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
