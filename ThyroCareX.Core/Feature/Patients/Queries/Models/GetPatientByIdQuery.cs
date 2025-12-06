using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Patients.Queries.Result;

namespace ThyroCareX.Core.Feature.Patients.Queries.Models
{
    public class GetPatientByIdQuery:IRequest<Response<GetPatientByIdResponse>>
    {
        public int PatientID { get; set; }
        public GetPatientByIdQuery(int patientID)
        {
            PatientID = patientID;
        }
    }
}
