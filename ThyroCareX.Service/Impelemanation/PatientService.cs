using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Service.Impelemanation
{
    public class PatientService : IPatientService
    {
        public Task<string> AddAsync(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> EditAsync(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Task<List<Patient>> GetAllPatientsByDoctorAsync(int doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Patient>> GetAllPatientsIncudelWithDoctorAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Patient> GetPatientByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
