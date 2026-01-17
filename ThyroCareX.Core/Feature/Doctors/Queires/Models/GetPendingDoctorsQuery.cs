using MediatR;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Doctors.Queires.Result;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Core.Feature.Doctors.Queires.Models
{
    public class GetPendingDoctorsQuery : IRequest<Response<List<GetDoctorListResponse>>>
    {
    }
}
