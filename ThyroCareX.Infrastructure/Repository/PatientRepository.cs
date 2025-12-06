using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Infrastructure.Context;
using ThyroCareX.Infrastructure.InfrastructureBases;

namespace ThyroCareX.Infrastructure.Repository
{
    public class PatientRepository:GenericRepositoryAsync<Patient>, IPatientRepository
    {
        #region Prop
        public readonly DbSet<Patient> _patient;
        #endregion

        #region Constructor
        public PatientRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
            _patient = dbcontext.Set<Patient>();
        }
        #endregion
        #region Handle Functions
        public async Task<List<Patient>> GetAllPatientAsync()
        {
            return await _patient.Include(x=>x.Doctor).ToListAsync();
        }

        public async Task<List<Patient>> GetAllPatientByDoctorAsync(int doctorId)
        {
            return await _patient.Where(x => x.DoctorID == doctorId).Include(p => p.Doctor).ToListAsync();
        }
        #endregion

    }
}
