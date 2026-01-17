using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases; 
using MediatR;

namespace ThyroCareX.Core.Feature.Patients.Command.Model
{
    public class DeletePatientCommand : IRequest<Response<string>>
    {
        public int PatientID { get; set; }

        public DeletePatientCommand(int id)
        {
            PatientID = id;
        }
    }
}
