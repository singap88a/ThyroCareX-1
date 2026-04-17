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
    public class ContactRepo: GenericRepositoryAsync<Contact>, IContactRepo
    {
        public readonly DbSet<Contact> _contact;
        public ContactRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _contact= dbContext.Set<Contact>();
            
        }

    }
}
