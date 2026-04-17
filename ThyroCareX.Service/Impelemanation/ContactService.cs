using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Infrastructure.Repository;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Service.Impelemanation
{
    public class ContactService : IContactService
    {
        private  readonly IContactRepo _contactRepo;
        public ContactService(IContactRepo contactRepo)
        {
            _contactRepo = contactRepo;
        }

        public async Task<string> AddMessage(Contact contact)
        {
            await _contactRepo.AddAsync(contact);
            return "Message Added Successfully";
        }
    }
}
