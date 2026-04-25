using Microsoft.IdentityModel.Tokens;
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
    public class MedicalHistoryService : IMedicalHistoryServices
    {
        private readonly IMedicalHistoryRepo _medicalHistoryRepo;
        public MedicalHistoryService(IMedicalHistoryRepo medicalHistoryRepo)
        {
         
            _medicalHistoryRepo = medicalHistoryRepo;
        }
        public async Task<string> AddAsync(MedicalHistory medicalHistory)
        {
           await _medicalHistoryRepo.AddAsync(medicalHistory);
            return "Medical history added successfully.";
        }
    }
}
