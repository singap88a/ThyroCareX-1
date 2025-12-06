using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.InfrastructureBases;

namespace ThyroCareX.Infrastructure.Abstarct
{
    public interface IPatientRepository:IGenericRepositoryAsync<Patient>
    {
        Task<List<Patient>> GetAllPatientAsync();
        Task<List<Patient>> GetAllPatientByDoctorAsync(int doctorId);

    }
}
