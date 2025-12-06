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
    public class PatientService:IPatientService
    {
        #region Prop
        private readonly IPatientRepository _patientRepository;
        #endregion
        #region Constructor
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        #endregion
        #region Handle Functions
        public Task<List<Patient>> GetAllPatientsIncudelWithDoctorAsync()
        {
           var patients= _patientRepository.GetTableNoTracking().Include(p => p.Doctor).ToListAsync();
            return patients;

        }
        public async Task<List<Patient>> GetAllPatientsByDoctorAsync(int doctorId)
        {
           return await _patientRepository.GetTableNoTracking()
                .Where(p => p.DoctorID == doctorId)
                .ToListAsync();
            
        }
        public async Task<Patient> GetPatientByIdAsync(int id)
        {
           var patient= await _patientRepository.GetByIdAsync(id);
            return patient;
        }
        public async Task<string> AddAsync(Patient patient)
        {
            await _patientRepository.AddAsync(patient);
            return "Patient Added Successfully";
        }

        public async Task<string> EditAsync(Patient patient)
        {
            await _patientRepository.UpdateAsync(patient);
            return "Patient Updated Successfully";
        }

        #endregion
    }
}
