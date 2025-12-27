using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Service.Abstarct
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatientsIncudelWithDoctorAsync();
        Task<List<Patient>> GetAllPatientsByDoctorAsync(int doctorId);
        Task<Patient>GetPatientByIdAsync(int id);
        Task<string> AddAsync(Patient patient);
        Task<string> EditAsync(Patient patient);
        Task<string> DeleteAsync(Patient patient);
    }
}
