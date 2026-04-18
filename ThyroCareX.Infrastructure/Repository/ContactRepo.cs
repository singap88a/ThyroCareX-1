using Microsoft.EntityFrameworkCore;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Infrastructure.Context;
using ThyroCareX.Infrastructure.InfrastructureBases;

namespace ThyroCareX.Infrastructure.Repository
{
    public class ContactRepo : GenericRepositoryAsync<Contact>, IContactRepo
    {
        private readonly DbSet<Contact> _contactMessages;
        public ContactRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _contactMessages = dbContext.Set<Contact>();
        }
    }
}
